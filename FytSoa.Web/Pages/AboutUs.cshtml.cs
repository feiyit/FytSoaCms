using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages
{
    public class AboutUsModel : PageModel
    {
        private readonly ICacheService _cacheService;
        private readonly ICmsSiteService _siteService;
        public AboutUsModel(ICacheService cacheServicee, ICmsSiteService siteService)
        {
            _cacheService = cacheServicee;
            _siteService = siteService;
        }
        public CmsSite Site { get; set; }
        public void OnGet()
        {
            //获得站点信息
            if (_cacheService.Exists(CacheKey.WEBCMSSITE))
            {
                Site = _cacheService.GetCache<CmsSite>(CacheKey.WEBCMSSITE);
            }
            else
            {
                Site = _siteService.GetModelAsync(m => m.Guid == "78756a6c-50c8-47a5-b898-5d6d24a20327").Result.data;
                //加入到缓存
                _cacheService.SetCache(CacheKey.WEBCMSSITE, Site, DateTimeOffset.Now.AddDays(30));
            }
        }
    }
}