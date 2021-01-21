using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FytSoa.Service.Extensions;

namespace FytSoa.Service.Implements
{
    public class CmsVideoService : BaseService<CmsVideo>, ICmsVideoService
    {
        public async Task<ApiResult<Page<CmsVideo>>> GetWherePage(PageParm param)
        {
            var result = new ApiResult<Page<CmsVideo>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                result.data = await Db.Queryable<CmsVideo>()
                    .Where(m=>m.ParentId==param.id)
                    .WhereIF(!string.IsNullOrEmpty(param.key), m => m.Title.Contains(param.key) || m.SubTitle.Contains(param.key))
                    .WhereIF(param.attr == 1, m => m.IsTop)
                    .OrderBy(m => m.AddTime, SqlSugar.OrderByType.Desc)
                    .ToPageAsync(param.page, param.limit);
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }
    }
}
