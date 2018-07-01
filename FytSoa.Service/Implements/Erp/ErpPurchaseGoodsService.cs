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
    /// 采购单商品接口服务实现
    /// </summary>
    public class ErpPurchaseGoodsService : DbContext, IErpPurchaseGoodsService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpPurchaseGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                parm.Guid = Guid.NewGuid().ToString();
                var dbres = ErpPurchaseGoodsDb.Insert(parm);
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
        /// 添加多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddListAsync(List<ErpPurchaseGoods> parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                foreach (var item in parm)
                {
                    item.Guid = Guid.NewGuid().ToString();
                }
                var dbres = ErpPurchaseGoodsDb.InsertRange(parm.ToArray());
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
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpPurchaseGoodsDb.Update(m => new ErpPurchaseGoods() { IsDel = true }, m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpPurchaseGoods>> GetByGuidAsync(string parm)
        {
            var model = ErpPurchaseGoodsDb.GetById(parm);
            var res = new ApiResult<ErpPurchaseGoods>
            {
                statusCode = 200,
                data = model ?? new ErpPurchaseGoods() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpPurchaseGoods>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpPurchaseGoods>>();
            try
            {
                var query = Db.Queryable<ErpPurchaseGoods>()
                        .Where(m => !m.IsDel)
                        .WhereIF(!string.IsNullOrEmpty(parm.guid), m => m.PurchaseGuid == parm.guid)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Name.Contains(parm.key) || m.Number == parm.key)
                        .OrderBy(m => m.Number, OrderByType.Desc)
                        .ToPageAsync(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data = await query;
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
        public async Task<ApiResult<string>> ModifyAsync(ErpPurchaseGoods parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpPurchaseGoodsDb.Update(parm);
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "修改数据失败~";
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
