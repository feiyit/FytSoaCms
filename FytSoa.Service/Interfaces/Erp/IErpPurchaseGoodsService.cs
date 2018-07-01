using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 采购单商品接口服务
    /// </summary>
    public interface IErpPurchaseGoodsService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<ErpPurchaseGoods>>> GetPagesAsync(PageParm parm);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<ErpPurchaseGoods>> GetByGuidAsync(string parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(ErpPurchaseGoods parm);

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddListAsync(List<ErpPurchaseGoods> parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(ErpPurchaseGoods parm);
    }
}
