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
    public class SysMenuService : DbContext, ISysMenuService
    {
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(SysMenu parm)
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
        /// 删除部门
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var list = Utils.StrToListString(parm);
            var isok = SysMenuDb.Delete(m => list.Contains(m.Guid));
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
        public async Task<ApiResult<SysMenu>> GetByGuidAsync(string parm)
        {
            var model = SysMenuDb.GetById(parm);
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
        public async Task<ApiResult<List<SysMenuTree>>> GetListTreeAsync()
        {
            var list = SysMenuDb.GetList();
            var treeList = new List<SysMenuTree>();
            foreach (var item in list.Where(m => m.Layer == 1).OrderBy(m => m.Sort))
            {
                //获得子级
                var children = RecursionOrganize(list, new List<SysMenuTree>(), item.Guid);
                treeList.Add(new SysMenuTree()
                {
                    guid = item.Guid,
                    name = item.Name,
                    open = children.Count > 0,
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
        List<SysMenuTree> RecursionOrganize(List<SysMenu> sourceList, List<SysMenuTree> list, string guid)
        {
            foreach (var row in sourceList.Where(m => m.ParentGuid == guid).OrderBy(m => m.Sort))
            {
                var res = RecursionOrganize(sourceList, new List<SysMenuTree>(), row.Guid);
                list.Add(new SysMenuTree()
                {
                    guid = row.Guid,
                    name = row.Name,
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
        public async Task<ApiResult<Page<SysMenu>>> GetPagesAsync(string key)
        {
            var res = new ApiResult<Page<SysMenu>>();
            try
            {
                using (Db)
                {
                    var query = Db.Queryable<SysMenu>()
                        .WhereIF(!string.IsNullOrEmpty(key), m => m.ParentGuidList.Contains(key))
                        .OrderBy(m => m.Sort).ToPageAsync(1, 1000);
                    res.success = true;
                    res.message = "获取成功！";
                    var result = new List<SysMenu>();
                    ChildModule(query.Result.Items, result, null);
                    query.Result.Items = result;
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
