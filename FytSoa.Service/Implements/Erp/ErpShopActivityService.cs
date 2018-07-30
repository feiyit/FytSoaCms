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
                var dbres = ErpShopActivityDb.Update(m => new ErpShopActivity() { IsDel = true }, m => list.Contains(m.Guid));
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
        /// 根据店铺获得最新的活动
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ShopActivityApp>> GetByShopsAsync(string parm)
        {
            var res = new ApiResult<ShopActivityApp>
            {
                statusCode = 200,
                data = null
            };
            var nowTime = DateTime.Now;
            //先查询店铺是否有活动，如果没有在查询全局是否有活动
            var shopActivity = ErpShopActivityDb.GetSingle(m=>!m.IsDel && m.Enable && m.ShopGuid==parm && SqlFunc.Between(nowTime, m.BeginDate, m.EndDate));
            if (shopActivity != null)
            {
                res.data = new ShopActivityApp()
                {
                    Guid= shopActivity.Guid,
                    Method= (shopActivity.Method == 1 ? "打折" : "满减"),
                    CountNum= shopActivity.CountNum,
                    FullBack= shopActivity.FullBack
                };
                return await Task.Run(() => res);
            }
            //查询全局活动
            var platformActivity = ErpShopActivityDb.GetSingle(m => !m.IsDel && m.Enable && m.ShopGuid == "all" && SqlFunc.Between(nowTime, m.BeginDate, m.EndDate));
            if (platformActivity!=null)
            {
                res.data = new ShopActivityApp()
                {
                    Guid = platformActivity.Guid,
                    Method = (platformActivity.Method == 1 ? "打折" : "满减"),
                    CountNum = platformActivity.CountNum,
                    FullBack = platformActivity.FullBack
                };
            }
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
                var nowTime = DateTime.Now;
                var query = Db.Queryable<ErpShopActivity>()
                        .Where(m=>!m.IsDel)
                        .WhereIF(parm.types==1,m=> SqlFunc.Between(nowTime,m.BeginDate,m.EndDate))
                        .Where(m=>m.ShopGuid==parm.guid || m.ShopGuid=="all")
                        .OrderBy(m => m.AddDate, OrderByType.Desc).Select(m => new ShopActivityDto()
                        {
                            Guid = m.Guid,
                            Types=m.Types,
                            TypeName = SqlFunc.ToString(m.Types),
                            MethodName = SqlFunc.ToString(m.Method),
                            CountNum = m.CountNum,
                            BeginDate = m.BeginDate,
                            Enable=m.Enable,
                            EndDate = m.EndDate
                        }).ToPageAsync(parm.page, parm.limit);
                if (query.Result.TotalItems!=0)
                {
                    foreach (var item in query.Result.Items)
                    {
                        item.Status = DateTime.Now > item.EndDate ? "已完成" : "进行中";
                        switch (item.Types)
                        {
                            case 0: item.TypeName = "全部加盟商"; break;
                            case 1: item.TypeName = "商铺"; break;
                            case 2: item.TypeName = "品牌"; break;
                        }
                        item.MethodName = item.MethodName == "1" ? "打折" : "满减";
                    }
                }
                
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

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyStatusAsync(ErpShopActivity parm)
        {
            var isok = ErpShopActivityDb.Update(
                m => new ErpShopActivity()
                {
                    Enable = parm.Enable
                }, m => m.Guid == parm.Guid);
            var res = new ApiResult<string>
            {
                success = isok,
                statusCode = isok ? (int)ApiEnum.Status : (int)ApiEnum.Error,
                data = isok ? "1" : "0"
            };
            return await Task.Run(() => res);
        }
    }
}
