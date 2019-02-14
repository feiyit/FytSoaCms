using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Cms;
using FytSoa.Extensions;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    public class CmsImageService  : BaseServer<CmsImage>, ICmsImageService
    {
        public CloudFile GetList(PageParm parm)
        {
            var model = new CloudFile() { Code = 200};
            try
            {
                var query = Db.Queryable<CmsImage>()
                        .WhereIF(parm.where!="/",m=>m.ImgBig.Contains(parm.where))
                        .OrderBy(m=>m.AddDate,OrderByType.Desc)
                        .ToPageAsync(parm.page, parm.limit);
                var fileList = new List<ListInfo>();
                if (query.Result.TotalItems != 0)
                {
                    foreach (var item in query.Result.Items)
                    {
                        fileList.Add(new ListInfo()
                        {
                            Name = item.ImgBig,
                            Size = item.ImgSize,
                            Type = item.ImgType,
                            Time = item.AddDate
                        });
                    }
                }
                model.list = fileList;
            }
            catch (Exception ex)
            {
                model.Message = ApiEnum.Error.GetEnumText() + ex.Message;
                model.Code = (int)ApiEnum.Error;
            }
            return model;
        }
    }
}
