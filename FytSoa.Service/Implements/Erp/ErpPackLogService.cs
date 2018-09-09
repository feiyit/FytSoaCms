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
    public class ErpPackLogService : DbContext, IErpPackLogService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpPackLog parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断是否存在
                var isExt = ErpPackLogDb.IsAny(m => m.PackName == parm.PackName);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该信息已存在~";
                }
                else
                {
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = ErpPackLogDb.Insert(parm);
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
                var dbres = ErpPackLogDb.Update(m => new ErpPackLog() { IsDel = true }, m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpPackLog>> GetByGuidAsync(string parm)
        {
            var model = ErpPackLogDb.GetById(parm);
            var res = new ApiResult<ErpPackLog>
            {
                statusCode = 200,
                data = model ?? new ErpPackLog() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<PackLogDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<PackLogDto>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time) && parm.time.Contains('-'))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                int years = !string.IsNullOrEmpty(parm.time) ? Convert.ToInt32(parm.time) : DateTime.Now.Year;
                var query = Db.Queryable<ErpPackLog>()
                        .Where(m => !m.IsDel)
                        .WhereIF(parm.types != 0, m => m.Types == parm.types)
                        .WhereIF(!string.IsNullOrEmpty(parm.guid),m=>m.ShopGuid==parm.guid)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.PackName.Contains(parm.key) || m.Number == parm.key)
                        .WhereIF(!string.IsNullOrEmpty(parm.time) && !parm.time.Contains('-'), m => m.AddDate.Year == years)
                        .WhereIF(!string.IsNullOrEmpty(parm.time) && parm.time.Contains('-'), m => m.AddDate >= Convert.ToDateTime(beginTime) && m.AddDate <= Convert.ToDateTime(endTime))
                        .OrderBy(m => m.AddDate, OrderByType.Desc).Select(m => new PackLogDto()
                        {
                            Guid = m.Guid,
                            Number = m.Number,
                            PackName = m.PackName,
                            ShopGuid = m.ShopGuid,
                            GoodsSum = m.GoodsSum,
                            ShopName = SqlFunc.Subqueryable<ErpShops>().Where(g => g.Guid == m.ShopGuid).Select(g => g.ShopName),
                            AddDate = m.AddDate
                        })
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
        public async Task<ApiResult<string>> ModifyAsync(ErpPackLog parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var dbres = ErpPackLogDb.Update(parm);
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
