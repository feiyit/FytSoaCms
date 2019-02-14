using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 字典分类
    /// </summary>
    public class SysCodeTypeService : DbContext, ISysCodeTypeService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(SysCodeType parm)
        {
            parm.Guid = Guid.NewGuid().ToString();
            parm.AddTime = DateTime.Now;
            parm.EditTime= DateTime.Now;
            parm.Layer = !string.IsNullOrEmpty(parm.ParentGuid) ? 1 : 0;
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = SysCodeTypeDb.Insert(parm) ?"1":"0"
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var isok = SysCodeTypeDb.Delete(m => m.Guid== parm);
            var res = new ApiResult<string>
            {
                statusCode = isok?200:500,
                data = isok ? "1" : "0",
                message = isok?"删除成功~":"删除失败~"
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<SysCodeTypeDto>> GetByGuidAsync(string parm)
        {
            var model = SysCodeTypeDb.GetSingle(m => m.Guid == parm);
            var res = new ApiResult<SysCodeTypeDto>
            {
                statusCode = 200,
                data = model!=null?new SysCodeTypeDto()
                {
                    guid=model.Guid,
                    name=model.Name,
                    parent = model.ParentGuid,
                    sort=model.Sort
                }:null
            };
            if (model != null) return await Task.Run(() => res);
            var pmdel = Db.Queryable<SysCodeType>().OrderBy(m => m.Sort,OrderByType.Desc).First();
            res.data=new SysCodeTypeDto(){sort = pmdel?.Sort + 1 ?? 1 };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysCodeType>>> GetListAsync()
        {
            var res = new ApiResult<List<SysCodeType>>
            {
                statusCode = 200,
                data = Db.Queryable<SysCodeType>().OrderBy(m=>m.Sort).ToList()
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得树实现
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysCodeTypeTree>>> GetListTreeAsync()
        {
            var list = SysCodeTypeDb.GetList();
            var treeList = new List<SysCodeTypeTree>();
            foreach (var item in list.Where(m=>m.Layer==0).OrderBy(m=>m.Sort))
            {
                //获得子级
                var children= new List<SysCodeTypeTree>();
                foreach (var row in list.Where(m => m.ParentGuid == item.Guid).OrderBy(m => m.Sort))
                {
                    children.Add(new SysCodeTypeTree()
                    {
                        guid = row.Guid,
                        name = row.Name,
                        children = null
                    });
                }
                treeList.Add(new SysCodeTypeTree() {
                    guid=item.Guid,
                    name=item.Name,
                    children= children
                });
            }
            var res = new ApiResult<List<SysCodeTypeTree>>
            {
                statusCode = 200,
                data = treeList
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(SysCodeType parm)
        {
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = SysCodeTypeDb.Update(m=>new SysCodeType() {Name=parm.Name, EditTime=DateTime.Now },m=>m.Guid==parm.Guid) ? "1" : "0"
            };
            return await Task.Run(() => res);
        }
    }
}
