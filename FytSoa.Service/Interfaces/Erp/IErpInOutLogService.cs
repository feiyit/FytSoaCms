using FytSoa.Common;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 出入库日志服务接口
    /// </summary>
    public interface IErpInOutLogService
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<ErpInOutLog>>> GetPagesAsync(PageParm parm, SearchParm searchParm);

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<ErpInOutLog>> GetByGuidAsync(string parm);

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> AddAsync(ErpInOutLog parm);

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteAsync(string parm);

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> ModifyAsync(ErpInOutLog parm);
    }
}
