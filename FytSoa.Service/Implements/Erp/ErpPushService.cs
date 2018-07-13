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
    /// 推送服务接口实现
    /// </summary>
    public class ErpPushService : DbContext, IErpPushService
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpPush parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                parm.Guid = Guid.NewGuid().ToString();
                var dbres = ErpPushDb.Insert(parm);
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "插入数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除一条或多条记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpPushDb.Delete(m => list.Contains(m.Guid));
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "删除数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<Page<ErpPush>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpPush>>();
            try
            {
                var query = Db.Queryable<ErpPush>().OrderBy(m => m.AddDate, OrderByType.Desc)
                    .ToPage(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data = query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }
        
    }
}
