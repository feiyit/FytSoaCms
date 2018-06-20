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
using Newtonsoft.Json;

namespace FytSoa.Service.Implements
{
    public class ErpShopActivityService : DbContext, IErpShopActivityService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpShopActivity parm, ShopActivityParm fullParm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断该活动是否冲突
                var isExt = ErpShopActivityDb.IsAny(m => m.BeginDate>=parm.BeginDate && m.EndDate>=parm.EndDate && m.Method == parm.Method && m.ShopGuid==parm.ShopGuid);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该活动已存在~";
                }
                else
                {
                    if (parm.Method==2)
                    {
                        var actList = new List<ShopActivity>();
                        for (int i = 0; i < fullParm.fullbegin.Count; i++)
                        {
                            actList.Add(new ShopActivity() {
                                fullbegin=!string.IsNullOrEmpty(fullParm.fullbegin[i])?int.Parse(fullParm.fullbegin[i]):0,
                                fullend = !string.IsNullOrEmpty(fullParm.fullend[i]) ? int.Parse(fullParm.fullend[i]) : 0,
                            });
                        }
                        parm.FullBack = JsonConvert.SerializeObject(actList);
                    }
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = ErpShopActivityDb.Insert(parm);
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
                var dbres = ErpShopActivityDb.Delete(m => list.Contains(m.Guid));
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
        public async Task<ApiResult<ErpShopActivity>> GetByGuidAsync(string parm)
        {
            var model = ErpShopActivityDb.GetById(parm);
            var res = new ApiResult<ErpShopActivity>
            {
                statusCode = 200,
                data = model ?? new ErpShopActivity() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ShopActivityDto>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ShopActivityDto>>();
            try
            {
                using (Db)
                {
                    var query = Db.Queryable<ErpShopActivity>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key),
                        m=> m.ShopGuid == parm.key)
                        .OrderBy(m => m.AddDate).Select(g=>new ShopActivityDto() {
                            Guid=g.Guid,
                            TypeName="商铺",
                            MethodName= SqlFunc.IIF(g.Method == 1, "折扣", "满减"),
                            CountNum=g.CountNum,
                            BeginDate=g.BeginDate,
                            EndDate=g.EndDate,
                            Status=SqlFunc.IIF(SqlFunc.GetDate() > g.EndDate , "已完成" , "进行中")
                        }).ToPageAsync(parm.page, parm.limit);
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
        public async Task<ApiResult<string>> ModifyAsync(ErpShopActivity parm, ShopActivityParm fullParm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断登录账号和店铺名是否存在
                var isExt = ErpShopActivityDb.IsAny(m => m.ShopGuid==parm.ShopGuid && m.BeginDate >= parm.BeginDate && m.EndDate >= parm.EndDate && m.Method == parm.Method && m.Guid != parm.Guid);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该活动已存在~";
                }
                else
                {
                    if (parm.Method == 2)
                    {
                        var actList = new List<ShopActivity>();
                        for (int i = 0; i < fullParm.fullbegin.Count; i++)
                        {
                            actList.Add(new ShopActivity()
                            {
                                fullbegin = !string.IsNullOrEmpty(fullParm.fullbegin[i]) ? int.Parse(fullParm.fullbegin[i]) : 0,
                                fullend = !string.IsNullOrEmpty(fullParm.fullend[i]) ? int.Parse(fullParm.fullend[i]) : 0,
                            });
                        }
                        parm.FullBack = JsonConvert.SerializeObject(actList);
                    }
                    var dbres = ErpShopActivityDb.Update(parm);
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
