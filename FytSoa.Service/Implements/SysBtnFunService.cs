using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Sys;
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
    /// 菜单功能实现
    /// </summary>
    public class SysBtnFunService : BaseServer<SysBtnFun>, ISysBtnFunService
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public new async Task<ApiResult<string>> AddAsync(SysBtnFun parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断功能值如果一样不允许添加
                var isExt = SysBtnFunDb.IsAny(m=>m.MenuGuid==parm.MenuGuid && m.FunType==parm.FunType);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该菜单功能已存在~";
                }
                else
                {
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = SysBtnFunDb.Insert(parm);
                    if (!dbres)
                    {
                        res.statusCode = (int)ApiEnum.Error;
                        res.message = "插入数据失败~";
                    }
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
        /// 查询多条记录，包含授权状态的
        /// </summary>
        /// <param name="key">角色的Guid</param>
        /// <param name="menuKey">菜单的Guid</param>
        /// <returns></returns>
        public Task<ApiResult<Page<SysBtnFunDto>>> GetPagesAsync(string key, string menuKey)
        {
            var res = new ApiResult<Page<SysBtnFunDto>>();
            try
            {
                var query = Db.Queryable<SysBtnFun>()
                        .Where(m => m.MenuGuid.Contains(menuKey))
                        .Select(it => new SysBtnFunDto()
                        {
                            Guid = SqlFunc.GetSelfAndAutoFill(it.Guid),
                            Status = SqlFunc.Subqueryable<SysPermissions>().Where(g => g.BtnFunGuid == it.Guid && g.Types == 3 && g.MenuGuid == menuKey && g.RoleGuid == key).Any()
                        })
                        .ToPage(1, 100);
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
