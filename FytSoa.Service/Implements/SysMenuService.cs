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
    public class SysMenuService : BaseServer<SysMenu>, ISysMenuService
    {
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public new async Task<ApiResult<string>> AddAsync(SysMenu parm)
        {
            parm.Guid = Guid.NewGuid().ToString();
            parm.EditTime = DateTime.Now;
            parm.AddTIme = DateTime.Now;
            SysMenuDb.Insert(parm);
            if (!string.IsNullOrEmpty(parm.ParentGuid))
            {
                // 说明有父级  根据父级，查询对应的模型
                var model = SysMenuDb.GetById(parm.ParentGuid);
                parm.ParentGuidList = model.ParentGuidList + parm.Guid + ",";
                parm.Layer = model.Layer + 1;
            }
            else
            {
                parm.ParentGuidList = "," + parm.Guid + ",";
                parm.Layer = 1;
            }
            //更新  新的对象
            SysMenuDb.Update(parm);
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = "1"
            };
            return await Task.Run(() => res);
        }


        /// <summary>
        /// 根据唯一编号查询一条部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysMenu>> GetByGuidAsync(string parm)
        {
            var model = SysMenuDb.GetSingle(m=>m.Guid==parm);
            var res = new ApiResult<SysMenu>
            {
                statusCode = 200
            };
            var pmdel = Db.Queryable<SysMenu>().OrderBy(m => m.Sort, OrderByType.Desc).First();
            res.data = model ?? new SysMenu() { Sort = pmdel?.Sort + 1 ?? 1, Status = true };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 查询Tree
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenuTree>>> GetListTreeAsync(string roleGuid)
        {
            var list = Db.Queryable<SysMenu>().Select(m => new SysMenuTree()
            {
                guid = m.Guid,
                name = m.Name,
                layer = m.Layer,
                parentGuid = m.ParentGuid,
                sort=m.Sort,
                isChecked= SqlFunc.Subqueryable<SysPermissions>().Where(g => g.RoleGuid == roleGuid && g.MenuGuid==m.Guid && g.Types == 1).Any()
            }).ToList();
            var treeList = new List<SysMenuTree>();
            foreach (var item in list.Where(m => m.layer == 1).OrderBy(m => m.sort))
            {
                //获得子级
                var children = RecursionOrganize(list, new List<SysMenuTree>(), item.guid);
                treeList.Add(new SysMenuTree()
                {
                    guid = item.guid,
                    name = item.name,
                    open = children.Count > 0,
                    isChecked= item.isChecked,
                    children = children.Count == 0 ? null : children
                });
            }
            var res = new ApiResult<List<SysMenuTree>>
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
        List<SysMenuTree> RecursionOrganize(List<SysMenuTree> sourceList, List<SysMenuTree> list, string guid)
        {
            foreach (var row in sourceList.Where(m => m.parentGuid == guid).OrderBy(m => m.sort))
            {
                var res = RecursionOrganize(sourceList, new List<SysMenuTree>(), row.guid);
                list.Add(new SysMenuTree()
                {
                    guid = row.guid,
                    name = row.name,
                    isChecked=row.isChecked,
                    open = res.Count > 0,
                    children = res.Count > 0 ? res : null
                });
            }
            return list;
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        public new async Task<ApiResult<Page<SysMenu>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SysMenu>>();
            try
            {
                var query = Db.Queryable<SysMenu>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.ParentGuidList.Contains(parm.key))
                        .OrderBy(m => m.Sort).ToPageAsync(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                var result = new List<SysMenu>();
                ChildModule(query.Result.Items, result, null);
                query.Result.Items = result;
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
        /// 递归模块列表
        /// </summary>
        private void ChildModule(List<SysMenu> list, List<SysMenu> newlist, string parentId)
        {
            var result = list.Where(p => p.ParentGuid == parentId).OrderBy(p => p.Layer).ThenBy(p => p.Sort).ToList();
            if (!result.Any()) return;
            for (int i = 0; i < result.Count(); i++)
            {
                newlist.Add(result[i]);
                ChildModule(list, newlist, result[i].Guid);
            }
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(SysMenu parm)
        {
            parm.EditTime = DateTime.Now;
            if (!string.IsNullOrEmpty(parm.ParentGuid))
            {
                // 说明有父级  根据父级，查询对应的模型
                var model = SysMenuDb.GetById(parm.ParentGuid);
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
                data = SysMenuDb.Update(parm) ? "1" : "0"
            };
            return await Task.Run(() => res);
        }
    }
}
