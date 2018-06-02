using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.Implements;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FytSoa.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISysAdminService, SysAdminService>();
            services.AddTransient<ISysCodeService, SysCodeService>();
            services.AddTransient<ISysCodeTypeService, SysCodeTypeService>();
            services.AddTransient<ISysOrganizeService, SysOrganizeService>();
            services.AddTransient<ISysMenuService, SysMenuService>();
            services.AddTransient<ISysRoleService, SysRoleService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/fytadmin/login");
                o.AccessDeniedPath = new PathString("/error");
            });

            //¿çÓòÉèÖÃ
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
