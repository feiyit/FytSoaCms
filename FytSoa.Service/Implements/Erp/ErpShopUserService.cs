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
    public class ErpShopUserService : DbContext, IErpShopUserService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpShopUser parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断登录账号是否存在
                var isExt = ErpShopUserDb.IsAny(m => m.ShopGuid == parm.ShopGuid && m.Mobile == parm.Mobile);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该会员已存在~";
                }
                else
                {
                    parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = ErpShopUserDb.Insert(parm);
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
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpShopUserDb.Delete(m => list.Contains(m.Guid));
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
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ErpShopUser>> GetByGuidAsync(string parm)
        {
            var model = ErpShopUserDb.GetById(parm);
            var res = new ApiResult<ErpShopUser>
            {
                statusCode = 200,
                data = model ?? new ErpShopUser() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpShopUser>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpShopUser>>();
            try
            {
                using (Db)
                {
                    var query = Db.Queryable<ErpShopUser>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key),
                        m => m.Mobile== parm.key
                        || m.ShopGuid== parm.key
                        || m.UserName== parm.key)
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

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(ErpShopUser parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断登录账号和店铺名是否存在
                var isExt = ErpShopUserDb.IsAny(m => m.ShopGuid == parm.ShopGuid && m.Mobile == parm.Mobile && m.Guid != parm.Guid);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该商铺已存在~";
                }
                else
                {
                    parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                    var dbres = ErpShopUserDb.Update(parm);
                    if (!dbres)
                    {
                        res.statusCode = (int)ApiEnum.Error;
                        res.message = "修改数据失败~";
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
    }
}
