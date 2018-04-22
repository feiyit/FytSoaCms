using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using FytErp.Core.Model.ConfigModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace FytErp.Web.Pages
{
    public class IndexModel : PageModel
    {
        public DbConnection DbSetting { get; private set; }
        public void OnGet()
        {
            //获得配置文件中的DBConnection节点
            DbSetting = ConfigServices.Configuration.GetSection("DbConnection").Get<DbConnection>();
        }
    }
}
