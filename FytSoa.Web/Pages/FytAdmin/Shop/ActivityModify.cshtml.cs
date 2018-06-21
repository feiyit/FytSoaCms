using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FytSoa.Web.Pages.FytAdmin.Shop
{
    /// <summary>
    /// 活动修改
    /// </summary>
    public class ActivityModifyModel : PageModel
    {
        private readonly IErpShopActivityService _activityService;
        public ActivityModifyModel(IErpShopActivityService activityService)
        {
            _activityService = activityService;
        }

        [BindProperty]
        public ErpShopActivity activityModel { get; set; }
        public List<ShopActivity> shopActivity { get; set; }
        public void OnGet(string guid,string shop)
        {
            activityModel = _activityService.GetByGuidAsync(guid).Result.data;
            if (!string.IsNullOrEmpty(shop))
            {
                activityModel.ShopGuid = shop;
            }
            if (string.IsNullOrEmpty(guid))
            {
                activityModel.BeginDate = DateTime.Now;
                activityModel.EndDate = DateTime.Now.AddMonths(1);
            }
            else
            {
                //将满减活动Json字符串转换成对象
                if (activityModel.Method==2)
                {
                    shopActivity = JsonConvert.DeserializeObject<List<ShopActivity>>(activityModel.FullBack);
                }
            }
        }
    }
}