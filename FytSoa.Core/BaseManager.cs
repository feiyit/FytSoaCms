using FytSoa.Common;
using FytSoa.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service
{
    /// <summary>
    /// 数据访问层基类
    /// </summary>
    /// <typeparam name="DbModel"></typeparam>
    public abstract class BaseManager<DbModel> : DbContext where DbModel : class, new()
    {
        public virtual ApiResult<DbModel> Add(DbModel dbModel)
        {
            var result = new ApiResult<DbModel>();
            GetDb<DbModel>().Insert(dbModel);
            result.data = dbModel;
            return result;
        }

        //public virtual ApiResult<DbModel> Delete(DbModel dbModel)
        //{
        //    var result = new ApiResult<DbModel>();
        //    result.success = GetDb<DbModel>().FalseDelete(dbModel);
        //    return result;
        //}

        //public virtual ApiResult<DbModel> Delete(DbModel [] dbModels)
        //{
        //    var result = new ApiResult<DbModel>();
        //    result.success = GetDb<DbModel>().FalseDelete(dbModels);
        //    return result;
        //}

        public virtual ApiResult<DbModel> GetById(string id)
        {
            var result = new ApiResult<DbModel>();
            result.data = GetDb<DbModel>().GetById(id);
            return result;
        }

        public virtual ApiResult<DbModel> Update(DbModel DbModel)
        {
            var result = new ApiResult<DbModel>();
            result.data = DbModel;
            result.success = GetDb<DbModel>().Update(DbModel);
            return result;
        }
    }
}
