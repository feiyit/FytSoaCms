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
    }
}
