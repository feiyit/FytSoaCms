using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 店铺管理服务接口实现
    /// </summary>
    public class ErpShopsService : DbContext, IErpShopsService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpShops>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpShops>>();
            try
            {
                using (Db)
                {
                    var query = Db.Queryable<ErpShops>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), 
                        m => m.ShopName.Contains(parm.key)
                        || m.LoginName.Contains(parm.key)
                        || m.AdminName.Contains(parm.key)
                        || m.Mobile==parm.key
                        || m.ShopCity.Contains(parm.key))
                        .OrderBy(m => m.RegDate).ToPageAsync(parm.page, parm.limit);
                    res.success = true;
                    res.message = "获取成功！";
                    res.data = await query;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }
    }
}
