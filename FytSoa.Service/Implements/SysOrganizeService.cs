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
    /// 部门实现
    /// </summary>
    public class SysOrganizeService : DbContext, ISysOrganizeService
    {
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(SysOrganize parm)
        {
            parm.Guid = Guid.NewGuid().ToString();
            parm.EditTime = DateTime.Now;
            SysOrganizeDb.Insert(parm);
            if (!string.IsNullOrEmpty(parm.ParentGuid))
            {
                // 说明有父级  根据父级，查询对应的模型
                var model = SysOrganizeDb.GetById(parm.ParentGuid);
                parm.ParentGuidList = model.ParentGuidList + parm.Guid + ",";
                parm.Layer = model.Layer + 1;
            }
            else
            {
                parm.ParentGuidList= "," + parm.Guid + ",";
            }
            //更新  新的对象
            SysOrganizeDb.Update(parm);
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = "1"
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var list = Utils.StrToListString(parm);
            var isok = SysOrganizeDb.Delete(m => list.Contains(m.Guid));
            var res = new ApiResult<string>
            {
                statusCode = isok ? 200 : 500,
                data = isok ? "1" : "0",
                message = isok ? "删除成功~" : "删除失败~"
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 根据唯一编号查询一条部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysOrganize>> GetByGuidAsync(string parm)
        {
            var model = SysOrganizeDb.GetSingle(m => m.Guid == parm);
            var res = new ApiResult<SysOrganize>
            {
                statusCode = 200
            };
            var pmdel = Db.Queryable<SysOrganize>().OrderBy(m => m.Sort, OrderByType.Desc).First();
            res.data = model ?? new SysOrganize() { Sort = pmdel?.Sort + 1 ?? 1,Status=true };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 查询Tree
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysOrganizeTree>>> GetListTreeAsync()
        {
            var list = SysOrganizeDb.GetList();
            var treeList = new List<SysOrganizeTree>();
            foreach (var item in list.Where(m => m.Layer == 0).OrderBy(m => m.Sort))
            {
                //获得子级
                var children = RecursionOrganize(list,new List<SysOrganizeTree>(),item.Guid);
                treeList.Add(new SysOrganizeTree()
                {
                    guid = item.Guid,
                    name = item.Name,
                    open= children.Count>0,
                    children = children.Count==0?null:children
                });
            }
            var res = new ApiResult<List<SysOrganizeTree>>
            {
                statusCode = 200,
                data = treeList
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 递归部门
        /// </summary>
        /// <param name="sourceList">原数据</param>
        /// <param name="list">新集合</param>
        /// <param name="guid">父节点</param>
        /// <returns></returns>
        List<SysOrganizeTree> RecursionOrganize(List<SysOrganize> sourceList,List<SysOrganizeTree> list,string guid)
        {
            foreach (var row in sourceList.Where(m => m.ParentGuid == guid).OrderBy(m => m.Sort))
            {
                var res = RecursionOrganize(sourceList, new List<SysOrganizeTree>(), row.Guid);
                list.Add(new SysOrganizeTree()
                {
                    guid = row.Guid,
                    name = row.Name,
                    open=res.Count>0,
                    children = res.Count>0?res:null
                });
            }
            return list;
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<Page<SysOrganize>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SysOrganize>>();
            try
            {
                using (Db)
                {
                    var query = Db.Queryable<SysOrganize>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key),m=>m.ParentGuidList.Contains(parm.key))
                        .OrderBy(m => m.Sort).ToPageAsync(parm.page, parm.limit);
                    res.success = true;
                    res.message = "获取成功！";
                    res.data = await query;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }

        public async Task<ApiResult<string>> ModifyAsync(SysOrganize parm)
        {
            parm.EditTime = DateTime.Now;
            if (!string.IsNullOrEmpty(parm.ParentGuid))
            {
                // 说明有父级  根据父级，查询对应的模型
                var model = SysOrganizeDb.GetById(parm.ParentGuid);
                parm.ParentGuidList = model.ParentGuidList + parm.Guid + ",";
                parm.Layer = model.Layer + 1;
            }
            else
            {
                parm.ParentGuidList = "," + parm.Guid + ",";
            }
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = SysOrganizeDb.Update(parm) ? "1" : "0"
            };
            return await Task.Run(() => res);
        }
    }
}
