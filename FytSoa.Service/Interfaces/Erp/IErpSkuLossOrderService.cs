using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IErpSkuLossOrderService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<ErpSkuLossOrder>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(ErpSkuLossOrder parm);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<ErpSkuLossOrder>> GetByGuidAsync(string parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(ErpSkuLossOrder parm);
    }
}
