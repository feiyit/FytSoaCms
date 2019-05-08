using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Extensions;
using FytSoa.Service.Implements;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace FytSoa.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            this.Configuration = builder.Build();
            BaseConfigModel.SetBaseConfig(Configuration, env.ContentRootPath, env.WebRootPath);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //自定注册
            AddAssembly(services, "FytSoa.Service");

            //解决视图输出内容中文编码问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region 认证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/fytadmin/login");
            })
            //新增一个新的方案
            .AddCookie(CompanyAuthorizeAttribute.CompanyAuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/company/login");
            })
            .AddJwtBearer(JwtAuthorizeAttribute.JwtAuthenticationScheme, o =>
            {
                var jwtConfig = new JwtAuthConfigModel();
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidateLifetime = true,//是否验证超时  当设置exp和nbf时有效 同时启用ClockSkew 
                    ClockSkew = TimeSpan.FromSeconds(30),//注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟
                    ValidAudience = jwtConfig.Audience,//Audience
                    ValidIssuer = jwtConfig.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuth:SecurityKey"]))//拿到SecurityKey
                };
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // 如果过期，则把<是否过期>添加到，返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            #endregion

            #region 授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("App", policy => policy.RequireRole("App").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("AdminOrApp", policy => policy.RequireRole("Admin,App").Build());
            });
            #endregion

            #region 缓存配置
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();
            RedisHelper.Initialization(new CSRedis.CSRedisClient(Configuration["Cache:Configuration"]));
            #endregion

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/web/index", "/");
            });

            #region Swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "FytSoa API",
                    Contact = new Contact { Name = "feiyit", Email = "715515390@qq.com", Url = "http://www.feiyit.com" }
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "FytSoa.Web.xml");
                var entityXmlPath = Path.Combine(basePath, "FytSoa.Core.xml");
                options.IncludeXmlComments(xmlPath, true);
                options.IncludeXmlComments(entityXmlPath);
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();

                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                //添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                options.AddSecurityRequirement(security);
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    //jwt默认的参数名称
                    Name = "Authorization",
                    //jwt默认存放Authorization信息的位置(请求头中)
                    In = "header",
                    Type = "apiKey"
                });

            });
            #endregion

            #region CORS
            services.AddCors(c =>
            {
                c.AddPolicy("Any", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });

                c.AddPolicy("Limit", policy =>
                {
                    policy
                    .WithOrigins("localhost:4909")
                    .WithMethods("get", "post", "put", "delete")
                    //.WithHeaders("Authorization");
                    .AllowAnyHeader();
                });
            });
            #endregion

            #region 性能 压缩
            services.AddResponseCompression();
            #endregion

            //NLog 数据库配置
            //NLog.LogManager.Configuration.FindTargetByName<NLog.Targets.DatabaseTarget>("db").ConnectionString = Configuration.GetConnectionString("LogConnectionString");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            #region 解决Ubuntu Nginx 代理不能获取IP问题
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            #endregion

            //添加NLog  
            loggerFactory.AddNLog();
            //读取Nlog配置文件 
            env.ConfigureNLog("nlog.config");
            //Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FytSoa API V1");
            });
            //自定义异常处理
            app.UseMiddleware<ExceptionFilter>();
            //认证
            app.UseAuthentication();


            //性能压缩
            app.UseResponseCompression();

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
                //ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
                //{
                //  { ".apk","application/vnd.android.package-archive"},
                //  { ".nupkg","application/zip"}
                //})  //支持特殊文件下载处理
            });
            app.UseCookiePolicy();
            app.UseCors("Any");
            app.UseMvc();
        }

      
        /// <summary>  
        /// 自动注册服务——获取程序集中的实现类对应的多个接口
        /// </summary>
        /// <param name="services">服务集合</param>  
        /// <param name="assemblyName">程序集名称</param>
        public void AddAssembly(IServiceCollection services,string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType).ToList();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    if (interfaceType.Length==1)
                    {
                        services.AddTransient(interfaceType[0], item);
                    }
                    if (interfaceType.Length>1)
                    {
                        services.AddTransient(interfaceType[1], item);
                    }                    
                }
            }
        }
    }
}
