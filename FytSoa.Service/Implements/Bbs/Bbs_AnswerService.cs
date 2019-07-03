using System.Threading.Tasks;
using FytSoa.Common;
using FytSoa.Core.Model.Bbs;
using FytSoa.Core.Model.Member;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Extensions;
using FytSoa.Service.Interfaces;
using SqlSugar;

namespace FytSoa.Service.Implements
{
    /*!
    * 文件名称：Bbs_answer服务接口实现
    */
    public class Bbs_AnswerService : BaseServer<Bbs_Answer>, IBbs_AnswerService
    {
        public async Task<ApiResult<Page<Bbs_Answer>>> GetPageUser(PageParm param)
        {
            var res = new ApiResult<Page<Bbs_Answer>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data = await Db.Queryable<Bbs_Answer, Member, Member_Group>((b, m, g) => new
                            JoinQueryInfos(JoinType.Inner, b.UserGuid == m.Guid
                                , JoinType.Inner, m.Grade == g.Guid))
                    .WhereIF(!string.IsNullOrEmpty(param.guid), (b, m, g) => b.QuestionGuid == param.guid)  //问题
                    .OrderByIF(param.attr == 1, (b, m, g) => b.AddTime,OrderByType.Desc)  //热门排序
                    .OrderBy((b, m, g) => b.IsAdopt,OrderByType.Desc)
                    .Select((b, m, g) => new Bbs_Answer()
                    {
                        Guid = b.Guid,
                        IsAdopt = b.IsAdopt,
                        UserGuid = b.UserGuid,
                        NickName = m.NickName,
                        HeadPic = m.HeadPic,
                        GroupName = g.Name,
                        Content = b.Content,
                        AddTime = b.AddTime
                    })
                    .ToPageAsync(param.page, param.limit);
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (System.Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 用户详细里面的回答列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<AnswerDto>>> GetUserCenterAnswer(PageParm param)
        {
            var res = new ApiResult<Page<AnswerDto>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data = await Db.Queryable<Bbs_Answer,Bbs_Questions, Member,  Member_Group>((a,  b, m, g) => new
                            JoinQueryInfos(JoinType.Inner, a.QuestionGuid == b.Guid
                                                          , JoinType.Inner, a.UserGuid == m.Guid
                                                                            , JoinType.Inner, m.Grade == g.Guid))
                   
                    .WhereIF(!string.IsNullOrEmpty(param.guid), (a, b, m, g) => a.UserGuid==param.guid)
                    .OrderBy((a, b, m, g) => a.AddTime, OrderByType.Desc)
                    .Select((a, b, m, g) => new AnswerDto()
                    {
                        Guid = b.Guid,
                        UserGuid = m.Guid,
                        Title = b.Title,
                        EnTitle = b.EnTitle,
                        NickName = m.NickName,
                        HeadPic = m.HeadPic,
                        GroupName = g.Name,
                        LookSum = b.LookSum,
                        AnswerSum = b.AnswerSum,
                        Support = b.Support,
                        Tags = b.Tags,
                        Answer=a.Content,
                        AddTime = b.AddTime
                    })
                    .ToPageAsync(param.page, param.limit);
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (System.Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }
    }
}