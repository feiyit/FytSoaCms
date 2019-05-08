using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Core.Model.Wx;
using FytSoa.Service.DtoModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /*!
    * 文件名称：Wx_material服务接口
    */
	public interface IWxMaterialService : IBaseServer<WxMaterial>
	{
        /// <summary>
        /// 添加一条公众号素材信息
        /// </summary>
        /// <param name="WxId">微信ID</param>
        /// <param name="list">内容列表</param>
        /// <returns></returns>
        Task<ApiResult<string>> Add(WxMaterial model,List<Material> list);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="parm">参数  key=关键字  id=公众号  types=保存区域</param>
        /// <returns></returns>
        Task<ApiResult<Page<WxMaterial>>> GetPageList(PageParm parm);
    }
}