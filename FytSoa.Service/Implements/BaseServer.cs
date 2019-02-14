using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FytSoa.Service.Interfaces;

namespace FytSoa.Service.Implements
{
    public class BaseServer<T> : DbContext, IBaseServer<T> where T : class, new()
    {
        #region 添加操作
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(T parm)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var dbres = GetDb<T>().Insert(parm);
                if (!dbres)
                {
                    res.message = "插入数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddListAsync(List<T> parm)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var dbres = GetDb<T>().InsertRange(parm.ToArray());
                if (!dbres)
                {
                    res.message = "插入数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
        #endregion

        #region 查询操作
        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        public async Task<ApiResult<T>> GetModelAsync(Expression<Func<T, bool>> where)
        {
            var model = GetDb<T>().GetSingle(where);
            var res = new ApiResult<T>
            {
                statusCode = 200,
                data = model ?? new T() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        public async Task<ApiResult<T>> GetModelAsync(string parm)
        {
            var model = GetDb<T>().GetById(parm);
            var res = new ApiResult<T>
            {
                statusCode = 200,
                data = model ?? new T() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
		/// 获得列表——分页
		/// </summary>
		/// <param name="parm">PageParm</param>
		/// <returns></returns>
        public async Task<ApiResult<Page<T>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<T>>();
            try
            {
                var query = Db.Queryable<T>()
                        .ToPageAsync(parm.page, parm.limit);
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
        /// 分页
        /// </summary>
        /// <param name="parm">分页参数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序值</param>
        /// <param name="orderEnum">排序方式OrderByType</param>
        /// <returns></returns>
        public async Task<ApiResult<Page<T>>> GetPagesAsync(PageParm parm, Expression<Func<T, bool>> where,
            Expression<Func<T, object>> order, DbOrderEnum orderEnum)
        {
            var res = new ApiResult<Page<T>>();
            try
            {
                var query = Db.Queryable<T>()
                        .Where(where)
                        .OrderByIF((int)orderEnum == 1, order, SqlSugar.OrderByType.Asc)
                        .OrderByIF((int)orderEnum == 2, order, SqlSugar.OrderByType.Desc)
                        .ToPageAsync(parm.page, parm.limit);
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
		/// 获得列表
		/// </summary>
		/// <param name="parm">PageParm</param>
		/// <returns></returns>
        public async Task<ApiResult<List<T>>> GetListAsync(Expression<Func<T, bool>> where,
            Expression<Func<T, object>> order, DbOrderEnum orderEnum)
        {
            var res = new ApiResult<List<T>>();
            try
            {
                var query = Db.Queryable<T>()
                        .Where(where)
                        .OrderByIF((int)orderEnum == 1, order, SqlSugar.OrderByType.Asc)
                        .OrderByIF((int)orderEnum == 2, order, SqlSugar.OrderByType.Desc)
                        .ToList();
                res.success = true;
                res.message = "获取成功！";
                res.data = query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得列表，不需要任何条件
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<T>>> GetListAsync()
        {
            var res = new ApiResult<List<T>>();
            try
            {
                var query = Db.Queryable<T>()
                        .ToList();
                res.success = true;
                res.message = "获取成功！";
                res.data = query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }
        #endregion

        #region 修改操作
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdateAsync(T parm)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var dbres = GetDb<T>().Update(parm);
                if (!dbres)
                {
                    res.message = "修改数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
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
        /// 修改一条数据，可用作假删除
        /// </summary>
        /// <param name="columns">修改的列=Expression<Func<T,T>></param>
        /// <param name="where">Expression<Func<T,bool>></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdateAsync(Expression<Func<T, T>> columns,
            Expression<Func<T, bool>> where)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var dbres = GetDb<T>().Update(columns, where);
                if (!dbres)
                {
                    res.message = "修改数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="parm">string</param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = GetDb<T>().DeleteByIds(list.ToArray());
                if (!dbres)
                {
                    res.message = "删除数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(Expression<Func<T, bool>> where)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var dbres = GetDb<T>().Delete(where);
                if (!dbres)
                {
                    res.message = "删除数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="parm">ID集合</param>
        /// <param name="columns">假删除列</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> where)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var dbres = GetDb<T>().Update(columns,where);
                if (!dbres)
                {
                    res.message = "删除数据失败~";
                }
                else
                {
                    res.statusCode = (int)ApiEnum.Status;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
        #endregion
    }
}
