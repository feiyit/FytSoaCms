using System;
using System.Collections.Generic;
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
    * 文件名称：Bbs_questions服务接口实现
    */
    public class Bbs_QuestionsService : BaseServer<Bbs_Questions>, IBbs_QuestionsService
    {
        /// <summary>
        /// 分页查询问题列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<Bbs_Questions>>> GetPageList(PageParm parm)
        {
            var res = new ApiResult<Page<Bbs_Questions>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data = await Db.Queryable<Bbs_Questions, Member, Bbs_Classify,Member_Group>((b, m, c,g) => new
                           JoinQueryInfos(JoinType.Inner, b.UserGuid == m.Guid
                                                         , JoinType.Inner, b.Types == c.Guid
                                                                           ,JoinType.Inner,m.Grade==g.Guid))
                    .WhereIF(!string.IsNullOrEmpty(parm.number), (b, m, c, g) =>b.EnTitle==parm.number)
                    .WhereIF(!string.IsNullOrEmpty(parm.key), (b, m, c, g) => b.Title.Contains(parm.key) || m.NickName.Contains(parm.key) || b.Tags.Contains(parm.key))
                    .WhereIF(!string.IsNullOrEmpty(parm.guid), (b, m, c, g) => b.Types == parm.guid)  //分类
                    .WhereIF(!string.IsNullOrEmpty(parm.where) && parm.where=="whd", (b, m, c, g) => b.AnswerSum==0) //未回答
                    .WhereIF(!string.IsNullOrEmpty(parm.where) && parm.where == "wjj", (b, m, c, g) => b.Status == 0) //未解决
                    .WhereIF(parm.audit==1, (b, m, c, g) => b.Audit)  //审核
                    .WhereIF(parm.audit == 0, (b, m, c, g) => !b.Audit) //未审核
                    .WhereIF(!string.IsNullOrEmpty(parm.where) && parm.where == "red", (b, m, c, g) => b.IsRed) //热门
                    .OrderByIF(parm.attr==1, (b, m, c, g) =>b.IsRed)  //热门排序
                    .OrderBy((b, m, c, g) =>b.AddTime,OrderByType.Desc)
                    .Select((b, m, c, g) => new Bbs_Questions()
                    {
                        Guid = b.Guid,
                        UserGuid=m.Guid,
                        Title = b.Title,
                        EnTitle = b.EnTitle,
                        NickName = m.NickName,
                        HeadPic = m.HeadPic,
                        GroupName = g.Name,
                        Types = c.ClassName,
                        LookSum = b.LookSum,
                        AnswerSum = b.AnswerSum,
                        Support = b.Support,
                        Status = b.Status,
                        IsRed = b.IsRed,
                        Tags = b.Tags,
                        Audit = b.Audit,
                        Contents = b.Contents,
                        Summary = b.Summary,
                        AddTime = b.AddTime
                    })
                    .ToPageAsync(parm.page, parm.limit);
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (System.Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 审核，失败发送原因
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Audit(QuestionAuditParam param)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                if (!param.Status)
                {
                    var model = Db.Queryable<Bbs_Questions>().Single(m => m.Guid == param.Guid);
                    //增加一条消息
                    var noticeModel=new Bbs_Notice()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        UserGuid = model.UserGuid,
                        Content = param.Text
                    };
                    await Db.Insertable(noticeModel).ExecuteCommandAsync();
                }
              
                //审核成功，不发送消息
                await Db.Updateable<Bbs_Questions>().SetColumns(m => new Bbs_Questions() { Audit = param.Status })
                    .Where(m => m.Guid == param.Guid).ExecuteCommandAsync();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 前端提交发表问题
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Add(List<FormParam> param, string userGuid)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var t = "";
                for (var i = 3; i < param.Count; i++)
                {
                    t += param[i].value + ",";
                }
                var model=new Bbs_Questions()
                {
                    Guid = Guid.NewGuid().ToString(),
                    UserGuid = userGuid,
                    Title=param[0].value,
                    EnTitle= Utils.UniqueData.Gener(),
                    Types= param[1].value,
                    Tags=t.TrimEnd(new[]{','}),
                    Contents= param[2].value,
                    Summary = Utils.CutString(param[2].value,120)
                };
                await Db.Insertable(model).ExecuteCommandAsync();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }

            return res;
        }

        /// <summary>
        /// 前台右侧内容
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<PageRightDto>> GetRgithData()
        {
            var res = new ApiResult<PageRightDto>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var m = new PageRightDto() { };
                //问题总数
                m.QuestionCount =await Db.Queryable<Bbs_Questions>().CountAsync();
                //会员总数
                m.UserCount=await Db.Queryable<Member>().CountAsync();
                //常用标签
                m.TagList =await Db.Queryable<Bbs_Tags>()
                    .Where(s=>!s.IsDel)
                    .Select(s=>new TagsCount()
                    {
                        TagName = s.TagName,
                        EnTagName = s.EnTagName,
                        TagCount = SqlFunc.Subqueryable<Bbs_Questions>().Where(g=>g.Tags.Contains(s.TagName)).Count()
                    }).OrderBy(g=>g.TagCount,OrderByType.Desc)
                    .Take(8).ToListAsync();
                //热门话题
                m.RedQuestionList =await Db.Queryable<Bbs_Questions,Member>((q,u)=>new JoinQueryInfos(
                        JoinType.Inner,q.UserGuid==u.Guid
                        )).Where((q, u) => q.IsRed)
                    .OrderBy((q, u) => q.AnswerSum, OrderByType.Desc)
                    .Select((q, u) => new Bbs_Questions()
                    {
                        Guid = q.Guid,
                        EnTitle = q.EnTitle,
                        HeadPic = u.HeadPic,
                        Title = q.Title
                    })
                    .Take(6).ToListAsync();
                //推荐专家
                m.ExpertList =await Db.Queryable<Member>()
                    .Where(u=>!u.IsDel && u.Grade== "1fc19833-539a-40cf-a000-1f9037b8dae4")
                    .Select(u =>new MemberQuestion()
                    {
                        Guid = u.Guid,
                        NickName = u.NickName,
                        HeadPic = u.HeadPic,
                        AnswerCount=SqlFunc.Subqueryable<Bbs_Answer>().Where(g=>g.UserGuid==u.Guid).Count(),
                        AdoptCount= SqlFunc.Subqueryable<Bbs_Answer>().Where(g => g.UserGuid == u.Guid && g.IsAdopt).Count()
                    })
                    .OrderBy(g=>g.AnswerCount,OrderByType.Desc)
                    .Take(6).ToListAsync();
                res.data = m;
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 查询专家
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<MemberQuestion>>> GetPageExpert(PageParm param)
        {
            var res = new ApiResult<Page<MemberQuestion>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                res.data= await Db.Queryable<Member>()
                    .Where(u => !u.IsDel)
                    .WhereIF(!string.IsNullOrEmpty(param.key),u=>u.NickName.Contains(param.key))
                    .WhereIF(param.types==1,u=>u.Grade== "1fc19833-539a-40cf-a000-1f9037b8dae4") //专家
                    .OrderByIF(param.orderType == 1, u => u.RegTime, OrderByType.Desc)
                    .Select(u => new MemberQuestion()
                    {
                        Guid = u.Guid,
                        NickName = u.NickName,
                        HeadPic = u.HeadPic,
                        AddTime=u.RegTime,
                        AnswerCount = SqlFunc.Subqueryable<Bbs_Answer>().Where(g => g.UserGuid == u.Guid).Count(),
                        AdoptCount = SqlFunc.Subqueryable<Bbs_Answer>().Where(g => g.UserGuid == u.Guid && g.IsAdopt).Count()
                    })
                    .OrderByIF(param.orderType==0,g => g.AnswerCount, OrderByType.Desc) //按专家
                    .ToPageAsync(param.page,param.limit);
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 增加查看数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddLookSum(Bbs_Questions model)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                await Db.Updateable<Bbs_Questions>().SetColumns(m => new Bbs_Questions() {LookSum = model.LookSum})
                    .Where(m=>m.Guid==model.Guid)
                    .ExecuteCommandAsync();

                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return res;
        }
    }
}