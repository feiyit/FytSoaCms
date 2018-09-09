using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 销售订单详情接口
    /// </summary>
    public interface IErpSaleOrderGoodsService
    {
        /// <summary>
        /// 根据订单号查询订单详细
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SaleOrderGoodsDto>>> GetPagesAsync(PageParm parm, SearchParm searchParm);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SaleOrderGoodsDto>>> GetPagesListAsync(PageParm parm, SearchSaleOrderGoods searchParm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(List<ErpSaleOrderGoods> parm);
    }
}
