using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 图片管理业务接口
    /// </summary>
    public interface ICmsImageService : IBaseServer<CmsImage>
    {
        CloudFile GetList(PageParm parm);
    }
}
