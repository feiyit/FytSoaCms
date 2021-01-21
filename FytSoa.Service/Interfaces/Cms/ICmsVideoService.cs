using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    public interface ICmsVideoService : IBaseService<CmsVideo>
    {
        Task<ApiResult<Page<CmsVideo>>> GetWherePage(PageParm param);
    }
}
