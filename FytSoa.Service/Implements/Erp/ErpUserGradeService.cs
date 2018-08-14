using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 会员等级服务接口实现
    /// </summary>
    public class ErpUserGradeService : DbContext, IErpUserGradeService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpUserGrade parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断是否存在
                var isExt = ErpUserGradeDb.IsAny(m => m.Name == parm.Name);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该信息已存在~";
                }
                else
                {
                    //默认是积分，如果不是，积分赋值给其他，根据类型
                    switch (parm.IsTypes)
                    {
                        case 0: parm.ExpVal = parm.Point;break;
                        case 2: parm.Amount = parm.Point; break;
                    }
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = ErpUserGradeDb.Insert(parm);
                    if (!dbres)
                    {
                        res.statusCode = (int)ApiEnum.Error;
                        res.message = "插入数据失败~";
                    }
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpUserGradeDb.Update(m => new ErpUserGrade() { IsDel = true }, m => list.Contains(m.Guid));
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "删除数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ErpUserGrade>> GetByGuidAsync(string parm)
        {
            var model = ErpUserGradeDb.GetById(parm);
            var res = new ApiResult<ErpUserGrade>
            {
                statusCode = 200,
                data = model ?? new ErpUserGrade() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpUserGrade>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpUserGrade>>();
            try
            {
                var query = Db.Queryable<ErpUserGrade>()
                        .Where(m => !m.IsDel)
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Name.Contains(parm.key))
                        .OrderBy(m => m.AddDate, OrderByType.Desc).ToPageAsync(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data = await query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(ErpUserGrade parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //默认是积分，如果不是，积分赋值给其他，根据类型
                switch (parm.IsTypes)
                {
                    case 0: parm.ExpVal = parm.Point; break;
                    case 2: parm.Amount = parm.Point; break;
                }
                Db.Updateable<ErpUserGrade>().UpdateColumns(m=>new ErpUserGrade() {
                    EditDate=DateTime.Now,
                    Name=parm.Name,
                    Icon=parm.Icon,
                    IsTypes=parm.IsTypes,
                    ExpVal=parm.ExpVal,
                    Point=parm.Point,
                    Amount=parm.Amount,
                    DisCount=parm.DisCount,
                    Summary=parm.Summary
                }).Where(m=>m.Guid==parm.Guid);
                var dbres = ErpUserGradeDb.Update(parm);
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "修改数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
    }
}
