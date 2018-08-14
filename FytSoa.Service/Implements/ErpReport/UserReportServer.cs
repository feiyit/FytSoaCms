using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 用户注册统计报表服务接口实现
    /// </summary>
    public class UserReportServer: DbContext, IUserReportServer
    {
        /// <summary>
        /// 查询12个月，每个月的注册统计
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Task<ApiResult<List<UserRegReport>>> GetUserRegReport(PageParm parm)
        {
            var res = new ApiResult<List<UserRegReport>>();
            try
            {
                if (string.IsNullOrEmpty(parm.key))
                {
                    parm.key = DateTime.Now.Year.ToString();
                }
                var strSql = "select date_format(RegDate,'%m') AS `Months`,COUNT(1) as RegCount from erpshopuser "
                    + "where date_format(RegDate,'%Y')='" + parm.key + "' "
                    + "group by months";
                var query = Db.Ado.SqlQuery<UserRegReport>(strSql);
                if (query != null && query.Count > 0)
                {
                    for (int i = 1; i < 13; i++)
                    {
                        var month = "0";
                        if (i < 10) { month = month + i.ToString(); }
                        else { month = i.ToString(); }
                        if (query.Find(m => m.Months == month) == null)
                        {
                            query.Add(new UserRegReport() { Months = month });
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < 13; i++)
                    {
                        var month = "0";
                        if (i < 10) { month = month + i.ToString(); }
                        else { month = i.ToString(); }
                        query.Add(new UserRegReport() { Months = month });
                    }
                }
                res.data = query.OrderBy(m => m.Months).ToList();
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }

        /// <summary>
        /// 用户注册按性别统计
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public Task<ApiResult<List<UserRegReport>>> GetUserSexRegReport(PageParm parm)
        {
            var res = new ApiResult<List<UserRegReport>>();
            try
            {
                if (string.IsNullOrEmpty(parm.key))
                {
                    parm.key = DateTime.Now.Year.ToString();
                }
                var strSql = "select sex AS `Months`,COUNT(1) as RegCount from erpshopuser "
                    //+ "where date_format(RegDate,'%Y')='" + parm.key + "' "
                    + "group by sex";
                var query = Db.Ado.SqlQuery<UserRegReport>(strSql);                
                res.data = query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return Task.Run(() => res);
        }
    }
}
