using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 默认页报表中心服务实现
    /// </summary>
    public class DefaultReportService : DbContext, IDefaultReportService
    {
        /// <summary>
        /// 查询今日待办事项，销售统计
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<BackLogReport>> GetBackLogReport()
        {
            var res = new ApiResult<BackLogReport>() { statusCode= (int)ApiEnum.Error };
            try
            {
                DateTime dayTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString() + " 00:00:00");
                string startWeek = Utils.GetMondayDate().ToShortDateString();
                string endWeek = Utils.GetSundayDate().AddDays(1).ToShortDateString();
                var model = new BackLogReport
                {
                    //今日返货数量
                    ReturnCount = ErpReturnOrderDb.Count(m => SqlFunc.DateIsSame(m.AddDate, dayTime)),
                    //今日退货数量
                    BackCount = ErpBackGoodsDb.Count(m => SqlFunc.DateIsSame(m.AddDate, dayTime)),
                    //库存报警
                    StockPoliceCount = ErpGoodsSkuDb.Count(m => m.StockSum < 10),
                    //今日加入会员
                    JoinUserCount = ErpShopUserDb.Count(m => SqlFunc.DateIsSame(m.RegDate, dayTime)),
                    //今日销售金额
                    DaySaleMoney=Db.Queryable<ErpSaleOrder>().Where(m=> SqlFunc.DateIsSame(m.AddDate, dayTime)).Sum(m=>m.RealMoney),
                    //本周销售金额
                    WeekSaleMoney= Db.Queryable<ErpSaleOrder>().Where(m => SqlFunc.Between(m.AddDate, Utils.GetMondayDate(),Utils.GetSundayDate().AddDays(1))).Sum(m => m.RealMoney),
                    //本月销售金额
                    MonthSaleMoney= Db.Queryable<ErpSaleOrder>().Where(m => SqlFunc.DateIsSame(m.AddDate, dayTime,DateType.Month)).Sum(m => m.RealMoney)
                };
                res.data = model;
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 查询本周，每天的销售额和订单数
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<WeekSaleReport>> GetWeekSaleReport()
        {
            var res = new ApiResult<WeekSaleReport>() { statusCode = (int)ApiEnum.Error };
            try
            {
                string startWeek = Utils.GetMondayDate().ToShortDateString();
                string endWeek = Utils.GetSundayDate().AddDays(1).ToShortDateString();
                var weekCount= Db.Ado.SqlQuery<WeekDayRes>("select DATE_FORMAT(o.AddDate,'%Y-%m-%d') Days,count(1) Counts,SUM(o.RealMoney) as Money from erpsaleorder o where o.AddDate between '" + startWeek+"' and '"+endWeek+ "' group by Days");
                var moneyList = new List<decimal>();
                var orderList = new List<int>();
                for (int i = 1; i < 8; i++)
                {
                    var isEx = false;
                    foreach (var item in weekCount)
                    {
                        var day = Convert.ToDateTime(item.Days).DayOfWeek.ToString();
                        if (i == Utils.GetWeekByWeekName(day))
                        {
                            isEx = true;
                            orderList.Add(item.Counts);
                            moneyList.Add(item.Money);
                        }
                    }
                    if (!isEx)
                    {
                        orderList.Add(0);
                        moneyList.Add(0);
                    }
                }
                res.data = new WeekSaleReport() { Money = moneyList, Order= orderList };
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 查询店铺销售排行榜
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<List<ShopSaleTop>>> GetShopSaleTopReport()
        {
            var res = new ApiResult<List<ShopSaleTop>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var query=Db.Queryable<ErpSaleOrder,ErpShops>((eso,es)=>new object[] {
                    JoinType.Left,eso.ShopGuid==es.Guid
                })
                .Select((eso, es)=>new ShopSaleTop() {
                    ShopName=es.ShopName,
                    Money= SqlFunc.AggregateSum(eso.RealMoney),
                    Counts=SqlFunc.AggregateCount(eso.Counts)
                })
                .PartitionBy("ShopName")
                .OrderBy("Money desc")
                .Take(7);
                res.data = query.ToList();
                var total = res.data.Sum(m=>m.Money);
                foreach (var item in res.data)
                {
                    item.Ratio = Math.Round((item.Money / total * 100),2);
                }
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 查询品牌销售排行榜 前20
        /// </summary>
        /// <returns></returns>
        public Task<ApiResult<List<BrandSaleTop>>> GetBrandSaleTopReport()
        {
            var res = new ApiResult<List<BrandSaleTop>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var query = Db.Queryable<ErpGoodsSku, SysCode>((egs, sc) => new object[] {
                    JoinType.Left,egs.BrankGuid==sc.Guid
                })
                .Select((egs, sc) => new BrandSaleTop()
                {
                    BrandName = sc.Name,
                    Counts = egs.SaleSum,
                })
                .OrderBy("Counts desc")
                .Take(20);
                res.data = query.ToList();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return Task.Run(() => res);
        }
    }
}
