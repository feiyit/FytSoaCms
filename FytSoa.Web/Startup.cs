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
using FytSoa.Tasks;
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
using Quartz.Impl.AdoJobStore;
using Quartz.Impl.AdoJobStore.Common;

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
            //�Զ�ע��
            AddAssembly(services, "FytSoa.Service");

            //�����ͼ����������ı�������
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region ��֤
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/fytadmin/login");
            })
            //����һ���µķ���
            .AddCookie(BbsUserAuthorizeAttribute.BbsUserAuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/bbs/nologin");
            })
            .AddJwtBearer(JwtAuthorizeAttribute.JwtAuthenticationScheme, o =>
            {
                var jwtConfig = new JwtAuthConfigModel();
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidateLifetime = true,//�Ƿ���֤��ʱ  ������exp��nbfʱ��Ч ͬʱ����ClockSkew 
                    ClockSkew = TimeSpan.FromSeconds(30),//ע�����ǻ������ʱ�䣬�ܵ���Чʱ��������ʱ�����jwt�Ĺ���ʱ�䣬��������ã�Ĭ����5����
                    ValidAudience = jwtConfig.Audience,//Audience
                    ValidIssuer = jwtConfig.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuth:SecurityKey"]))//�õ�SecurityKey
                };
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // ������ڣ����<�Ƿ����>��ӵ�������ͷ��Ϣ��
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            #endregion

            #region ��Ȩ
            services.AddAuthorization(options =>
            {
                options.AddPolicy("App", policy => policy.RequireRole("App").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("AdminOrApp", policy => policy.RequireRole("Admin,App").Build());
            });
            #endregion

            #region ��������
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();
            RedisHelper.Initialization(new CSRedis.CSRedisClient(Configuration["Cache:Configuration"]));
            #endregion

            services.AddMvc().AddJsonOptions(option => {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddSingleton(GetScheduler());

            #region Swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "FytSoa API",
                    Contact = new Contact { Name = "feiyit", Email = "715515390@qq.com", Url = "" }
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "FytSoa.Web.xml");
                var entityXmlPath = Path.Combine(basePath, "FytSoa.Core.xml");
                options.IncludeXmlComments(xmlPath, true);
                options.IncludeXmlComments(entityXmlPath);
                //���header��֤��Ϣ
                //c.OperationFilter<SwaggerHeader>();

                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                //���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������Ҫһ�£�������Bearer��
                options.AddSecurityRequirement(security);
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT-Test: \"Authorization: Bearer {token}\"",
                    //jwtĬ�ϵĲ�������
                    Name = "Authorization",
                    //jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
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

            #region ���� ѹ��
            services.AddResponseCompression();
            #endregion

            //NLog ���ݿ�����
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

            //����
            app.UseStatusCodePagesWithReExecute("/Error");

            #region ���Ubuntu Nginx �����ܻ�ȡIP����
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            #endregion

            #region Nlog����־
            //����־��¼�����ݿ�
            NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
            NLog.LogManager.Configuration.Variables["connectionString"] = Configuration["DBConnection:MySqlConnectionString"];
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  //������־�е������������
            #endregion

            #region  ��ʼ����ţ�Ʋ���
            QiniuCloud.QnColud.Setting(new QiniuConfig()
            {
                AK=Configuration["QiNiu:AccessKey"],
                SK = Configuration["QiNiu:SecretKey"],
                Bucket = Configuration["QiNiu:Bucket"],
                BasePath = Configuration["QiNiu:BasePath"],
                domain = Configuration["QiNiu:Domain"]
            });
            #endregion

            #region Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FytSoa API V1");
            });
            #endregion

            //�Զ����쳣����
            //app.UseMiddleware<ExceptionFilter>();

            //��֤
            app.UseAuthentication();

            //����ѹ��
            app.UseResponseCompression();

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
                //ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
                //{
                //  { ".apk","application/vnd.android.package-archive"},
                //  { ".nupkg","application/zip"}
                //})  //֧�������ļ����ش���
            });
            app.UseCookiePolicy();
            app.UseCors("Any");
            app.UseMvc();
        }

      
        /// <summary>  
        /// �Զ�ע����񡪡���ȡ�����е�ʵ�����Ӧ�Ķ���ӿ�
        /// </summary>
        /// <param name="services">���񼯺�</param>  
        /// <param name="assemblyName">��������</param>
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

        private SchedulerCenter GetScheduler()
        {
            string dbProviderName = Configuration.GetSection("Quartz")["dbProviderName"];
            string connectionString = Configuration.GetSection("Quartz")["connectionString"];
            string driverDelegateType = string.Empty;
            switch (dbProviderName)
            {
                case "SQLite-Microsoft":
                case "SQLite":
                    driverDelegateType = typeof(SQLiteDelegate).AssemblyQualifiedName; break;
                case "MySql":
                    driverDelegateType = typeof(MySQLDelegate).AssemblyQualifiedName; break;
                case "OracleODPManaged":
                    driverDelegateType = typeof(OracleDelegate).AssemblyQualifiedName; break;
                case "SQLServer":
                case "SQLServerMOT":
                    driverDelegateType = typeof(SqlServerDelegate).AssemblyQualifiedName; break;
                case "Npgsql":
                    driverDelegateType = typeof(PostgreSQLDelegate).AssemblyQualifiedName; break;
                case "Firebird":
                    driverDelegateType = typeof(FirebirdDelegate).AssemblyQualifiedName; break;
                default:
                    throw new System.Exception("dbProviderName unreasonable");
            }
            SchedulerCenter schedulerCenter = SchedulerCenter.Instance;
            schedulerCenter.Setting(new DbProvider(dbProviderName, connectionString), driverDelegateType);
            return schedulerCenter;
        }
    }
}
