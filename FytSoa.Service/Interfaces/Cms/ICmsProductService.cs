using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    public interface ICmsProductService : IBaseService<CmsProduct>
    {
        Task<ApiResult<Page<CmsProduct>>> GetWherePage(PageParm param);
    }
}
