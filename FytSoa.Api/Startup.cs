using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.Implements;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FytSoa.Api
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
            services.AddTransient<IErpInOutLogService, ErpInOutLogService>();
            services.AddTransient<IErpPackLogService, ErpPackLogService>();
            services.AddTransient<IErpReturnGoodsService, ErpReturnGoodsService>();
            services.AddTransient<IErpBackGoodsService, ErpBackGoodsService>();
            services.AddTransient<IErpGoodsService, ErpGoodsService>();
            services.AddTransient<IErpGoodsSkuService, ErpGoodsSkuService>();
            services.AddTransient<IErpPushService, ErpPushService>();
            services.AddTransient<IErpShopsService, ErpShopsService>();
            services.AddTransient<IErpShopActivityService, ErpShopActivityService>();
            services.AddTransient<IErpShopUserService, ErpShopUserService>();
            services.AddTransient<IErpStaffService, ErpStaffService>();


            services.AddTransient<ISysBtnFunService, SysBtnFunService>();
            services.AddTransient<ISysPermissionsService, SysPermissionsService>();
            services.AddTransient<ISysLogService, SysLogService>();
            services.AddTransient<ISysAdminService, SysAdminService>();
            services.AddTransient<ISysCodeService, SysCodeService>();
            services.AddTransient<ISysCodeTypeService, SysCodeTypeService>();
            services.AddTransient<ISysOrganizeService, SysOrganizeService>();
            services.AddTransient<ISysMenuService, SysMenuService>();
            services.AddTransient<ISysRoleService, SysRoleService>();

            //跨域设置
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
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
