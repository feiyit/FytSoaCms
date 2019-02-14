using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            #region 注册服务
            services.AddTransient<ICmsSiteService, CmsSiteService>();
            services.AddTransient<ICmsImgTypeService, CmsImgTypeService>();
            services.AddTransient<ICmsImageService, CmsImageService>();
            services.AddTransient<ICmsColumnService, CmsColumnService>();
            services.AddTransient<ICmsTemplateService, CmsTemplateService>();
            services.AddTransient<ICmsArticleService, CmsArticleService>();
            services.AddTransient<ICmsAdvClassService, CmsAdvClassService>();
            services.AddTransient<ICmsAdvListService, CmsAdvListService>();
            services.AddTransient<ICmsMessageService, CmsMessageService>();
            services.AddTransient<ICmsDownloadService, CmsDownloadService>();

            services.AddTransient<ISysAppSettingService, SysAppSettingService>();
            services.AddTransient<ISysAuthorizeService, SysAuthorizeService>();
            services.AddTransient<ISysBtnFunService, SysBtnFunService>();
            services.AddTransient<ISysPermissionsService, SysPermissionsService>();
            services.AddTransient<ISysLogService, SysLogService>();
            services.AddTransient<ISysAdminService, SysAdminService>();
            services.AddTransient<ISysCodeService, SysCodeService>();
            services.AddTransient<ISysCodeTypeService, SysCodeTypeService>();
            services.AddTransient<ISysOrganizeService, SysOrganizeService>();
            services.AddTransient<ISysMenuService, SysMenuService>();
            services.AddTransient<ISysRoleService, SysRoleService>();
            #endregion

            //解决视图输出内容中文编码问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region 认证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/fytadmin/login");
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                JwtAuthConfigModel jwtConfig = new JwtAuthConfigModel();
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "FytSos",
                    ValidAudience = "wr",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.JWTSecretKey)),

                    /***********************************TokenValidationParameters的参数默认值***********************************/
                    RequireSignedTokens = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    // 是否要求Token的Claims中必须包含 Expires
                    RequireExpirationTime = true,
                    // 允许的服务器时间偏移量
                    // ClockSkew = TimeSpan.FromSeconds(300),
                    // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true
                };
            });
            #endregion

            #region 授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireApp", policy => policy.RequireRole("App").Build());
                options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("RequireAdminOrApp", policy => policy.RequireRole("Admin,App").Build());
            });
            #endregion

            #region 缓存 读取配置是否使用哪种缓存模式
            services.AddMemoryCache();
            if (Convert.ToBoolean(Configuration["Cache:IsUseRedis"]))
            {
                services.AddSingleton<ICacheService, RedisCacheService>();
            }
            else
            {
                services.AddSingleton<ICacheService, MemoryCacheService>();
            }
            #endregion

            #region 缓存 RedisCache
            //将Redis分布式缓存服务添加到服务中
            services.AddDistributedRedisCache(options =>
            {
                //用于连接Redis的配置 
                options.Configuration = "localhost";// Configuration.GetConnectionString("RedisConnectionString");
                //Redis实例名RedisDistributedCache
                options.InstanceName = "RedisInstance";
            });
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

            //授权
            app.UseMiddleware<JwtAuthorizationFilter>();

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
            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
