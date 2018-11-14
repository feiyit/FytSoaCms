using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 库存盘点服务接口
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// 库存盘点
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<StockInventory>>> GetPagesAsync(PageParm parm, SearchSaleOrderGoods searchParm);

        /// <summary>
        /// 加盟商库存盘点报表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<ShopInventory>>> GetShopInventoryAsync(PageParm parm, SearchSaleOrderGoods searchParm);

        /// <summary>
        /// 查询库存剩余数量和销售数量
        /// 可根据店铺查询，日期，品牌
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<StockSaleNum>>> GetStockNumByShopAsync(PageParm parm, AppSearchParm searchParm);

        /// <summary>
        /// 获得APP端统计的营业额
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<DayTurnover>> GetTurnover(PageParm parm, AppSearchParm searchParm);

        /// <summary>
        /// 获得加盟商统计的营业额
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<ShopTurnover>>> GetShopTurnover(PageParm parm);

        /// <summary>
        /// 获得月份的营业额，退货、返货
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<VMonthTurnover>>> GetMonthTurnover(PageParm parm);

        /// <summary>
        /// 获得加盟商列表，包含库存总数
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ShopStockReport>>> GetShopStockReport(PageParm parm);

        /// <summary>
        /// 平台入库统计报表，入库 总数，可根据年份查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<PlatformInOutStockReport>>> GetPlatformInStockReport(PageParm parm);

        /// <summary>
        /// 加盟商退货统计报表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ShopBackReturnReport>>> GetShopBackReport(PageParm parm);

        /// <summary>
        /// 加盟商返货统计报表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ShopBackReturnReport>>> GetShopReturnReport(PageParm parm);

    }
}
