using System.Threading.Tasks;
using FytSoa.Common;
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
        Task<ApiResult<Page<StockInventory>>> GetPagesAsync(PageParm parm);


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
    }
}
