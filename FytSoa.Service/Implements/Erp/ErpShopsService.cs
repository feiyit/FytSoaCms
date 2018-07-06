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
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpShops parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断登录账号和店铺名是否存在
                var isExt = ErpShopsDb.IsAny(m => m.LoginName == parm.LoginName && m.ShopName == parm.ShopName);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该商铺已存在~";
                }
                else
                {
                    parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = ErpShopsDb.Insert(parm);
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
                var dbres = ErpShopsDb.Delete(m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpShops>> GetByGuidAsync(string parm)
        {
            var model = ErpShopsDb.GetById(parm);
            var res = new ApiResult<ErpShops>
            {
                statusCode = 200,
                data = model ?? new ErpShops() { }
            };
            return await Task.Run(() => res);
        }

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

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(ErpShops parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断登录账号和店铺名是否存在
                var isExt = ErpShopsDb.IsAny(m => m.LoginName == parm.LoginName && m.ShopName == parm.ShopName && m.Guid!=parm.Guid);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该商铺已存在~";
                }
                else
                {
                    parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                    var dbres = ErpShopsDb.Update(parm);
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
