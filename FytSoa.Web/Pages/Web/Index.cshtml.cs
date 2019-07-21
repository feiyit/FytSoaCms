using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FytSoa.Web.Pages.Web
{
    public class IndexModel : PageModel
    {
        private readonly ICacheService _cacheService;
        private readonly ICmsSiteService _siteService;
        private readonly ICmsColumnService _columnService;
        private readonly ICmsArticleService _articleService;
        private readonly ICmsMessageService _messageService;
        public IndexModel(ICacheService cacheServicee, ICmsSiteService siteService, ICmsColumnService columnService
            , ICmsArticleService articleService, ICmsMessageService messageService)
        {
            _cacheService = cacheServicee;
            _siteService = siteService;
            _columnService = columnService;
            _articleService = articleService;
            _messageService = messageService;
        }
        public CmsSite Site { get; set; }

        public List<CmsColumn> Column { get; set; }

        public List<CmsAdvList> Adv { get; set; }

        public List<CmsArticle> Case { get; set; }

        public List<CmsArticle> Article { get; set; }

        public void OnGet()
        {
            //���վ����Ϣ
            if (_cacheService.Exists(CacheKey.WEBCMSSITE))
            {
                Site = _cacheService.GetCache<CmsSite>(CacheKey.WEBCMSSITE);
            }
            else
            {
                Site = _siteService.GetModelAsync(m => m.Guid == "78756a6c-50c8-47a5-b898-5d6d24a20327").Result.data;
                //���뵽����
                _cacheService.SetCache(CacheKey.WEBCMSSITE, Site, DateTimeOffset.Now.AddDays(30));
            }
            //�����Ŀ��Ϣ
            if (_cacheService.Exists(CacheKey.WEBCMSCOLUMN))
            {
                Column = _cacheService.GetCache<List<CmsColumn>>(CacheKey.WEBCMSCOLUMN);
            }
            else
            {
                Column = _columnService.GetListAsync(m => true, m => m.Sort, DbOrderEnum.Asc).Result.data;
                //���뵽����
                _cacheService.SetCache(CacheKey.WEBCMSCOLUMN, Column, DateTimeOffset.Now.AddDays(30));
            }

            //��ѯ��������Ȩ�غ���������
            var caseColumn = Column.Where(m => m.ParentId == 1015).Select(m => m.Id).ToList();
            Case = _articleService.WebGetList(new Service.DtoModel.PageParm() { limit = 10, types = 1, where = "istop=1" }, caseColumn).Items;

            //��ѯ���ţ���Ȩ�غ���������
            var articleColumn = Column.Where(m => m.ParentId == 1016).Select(m => m.Id).ToList();
            Article = _articleService.WebGetList(new Service.DtoModel.PageParm() { limit = 13, types = 1 }, articleColumn).Items;
        }

        /// <summary>
        ///  �û����ԣ��ύ����
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult OnGetMessage(CmsMessage model)
        {
            var apiRes = new ApiResult<string>() { statusCode = (int)ApiEnum.Status };
            try
            {
                model.IP = Utils.GetIp();
                var list = _messageService.GetListAsync(m => m.IP == model.IP, m => m.AddDate, DbOrderEnum.Asc).Result.data;
                if (list.Count > 3)
                {
                    return new JsonResult(new ApiResult<string>() { statusCode = (int)ApiEnum.HttpRequestError, message = "���ύ�Ĵ������࣬���Ժ����ԣ�~" });
                }

                model.ColumnId = "message";
                model.UserName = model.Title;
                model.AddDate = DateTime.Now;
                _messageService.AddAsync(model);
            }
            catch (Exception ex)
            {
                apiRes.message = ex.Message;
            }
            return new JsonResult(new ApiResult<string>() { statusCode = apiRes.statusCode, message = apiRes.message });
        }
    }
}