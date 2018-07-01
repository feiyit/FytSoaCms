using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 采购单接口服务
    /// </summary>
    public interface IErpPurchaseService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<PurchaseDto>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<ErpPurchase>> GetByGuidAsync(string parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(ErpPurchase parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(ErpPurchase parm);

        /// <summary>
        /// 修改采购单状态
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyStatusAsync(string parm);
    }
}
