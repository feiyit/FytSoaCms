using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.FytAdmin.Sale
{
    [Authorize]
    public class ReturnLossModifyModel : PageModel
    {
        private readonly IErpReturnLossService _lossService;
        public ReturnLossModifyModel(IErpReturnLossService lossService)
        {
            _lossService = lossService;
        }

        [BindProperty]
        public ErpReturnLoss LossModel { get; set; }

        public void OnGet(string guid)
        {
            LossModel = _lossService.GetByGuidAsync(guid).Result.data;
            if (string.IsNullOrEmpty(guid))
            {
                LossModel.AdminGuid= @User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Sid)?.Value;
            }
        }
    }
}