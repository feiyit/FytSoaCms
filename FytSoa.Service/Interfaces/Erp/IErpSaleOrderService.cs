using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 销售订单服务接口
    /// </summary>
    public interface IErpSaleOrderService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SaleOrderDto>>> GetPagesAsync(PageParm parm, AppSearchParm searchParm);

        /// <summary>
        /// 获得列表 不包含订单下面的商品列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SaleOrderDto>>> GetPagesNoGoodsAsync(PageParm parm, AppSearchParm searchParm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(ErpSaleOrder parm,string goodsJson);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<SaleOrderApp>> GetByNumberAsync(string parm);

        /// <summary>
        /// 残次品销售，上传图片
        /// </summary>
        /// <param name="guid">订单唯一编号Guid</param>
        /// <param name="imgPath">图片地址</param>
        /// <returns></returns>
        Task<ApiResult<string>> UpdateOrderAddImage(string guid,string imgPath);
    }
}
