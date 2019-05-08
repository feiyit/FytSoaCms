using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using Newtonsoft.Json;

namespace FytSoa.Service.Implements
{
    public class SysMenuService : BaseServer<SysMenu>, ISysMenuService
    {
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(SysMenu parm, List<string> btnfun)
        {
            var res = new ApiResult<string>
            {
                statusCode = 200,
                data = "1"
            };
            //判断别名是否存在，要不一样的
            var isCodeExis = SysMenuDb.GetSingle(m => m.NameCode == parm.NameCode);
            if (isCodeExis != null)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = "别名已存在~";
                return await Task.Run(() => res);
            }
            parm.Guid = Guid.NewGuid().ToString();
            parm.EditTime = DateTime.Now;
            parm.AddTIme = DateTime.Now;
            parm.BtnFunJson = JsonConvert.SerializeObject(btnfun);

            await Db.Insertable(parm).ExecuteCommandAsync();
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
            await Db.Updateable(parm).ExecuteCommandAsync() ;
            return res;
        }

        /// <summary>
        /// 根据唯一编号查询一条部门信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<SysMenu>> GetByGuidAsync(string parm)
        {
            var model =await Db.Queryable<SysMenu>().SingleAsync(m=>m.Guid==parm);
            var res = new ApiResult<SysMenu>
            {
                statusCode = 200
            };
            var pmdel = Db.Queryable<SysMenu>().OrderBy(m => m.Sort, OrderByType.Desc).First();
            res.data = model ?? new SysMenu() { Sort = pmdel?.Sort + 1 ?? 1, Status = true };
            return res;
        }

        /// <summary>
        /// 查询Tree
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenuTree>>> GetListTreeAsync(string roleGuid)
        {
            var list =await Db.Queryable<SysMenu>().Select(m => new SysMenuTree()
            {
                guid = m.Guid,
                name = m.Name,
                layer = m.Layer,
                parentGuid = m.ParentGuid,
                sort=m.Sort,
                isChecked= SqlFunc.Subqueryable<SysPermissions>().Where(g => g.RoleGuid == roleGuid && g.MenuGuid==m.Guid && g.Types == 1).Any()
            }).ToListAsync();
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
            return res;
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
        public async Task<ApiResult<Page<SysMenu>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<SysMenu>>();
            try
            {                
                var query =await Db.Queryable<SysMenu>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.ParentGuidList.Contains(parm.key))
                        .OrderBy(m => m.Sort)
                        .Mapper((it, cache)=> {
                            var codeList = cache.Get(t =>
                              {
                                  return Db.Queryable<SysCode>().Where(m=>m.ParentGuid== "a88fa4d3-3658-4449-8f4a-7f438964d716").ToList();
                              });
                            var list = new List<string>();
                            if (!string.IsNullOrEmpty(it.BtnFunJson))
                            {
                                list = JsonConvert.DeserializeObject<List<string>>(it.BtnFunJson);
                            }
                            if (list.Count>0)
                            {
                                it.BtnFunJson = string.Join(',', codeList.Where(g => list.Contains(g.Guid)).Select(g => g.Name).ToList());
                            }
                        })
                        .ToPageAsync(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                var result = new List<SysMenu>();
                if (!string.IsNullOrEmpty(parm.key))
                {
                    var menuModel = SysMenuDb.GetSingle(m => m.Guid == parm.key);
                    ChildModule(query.Items, result, menuModel.ParentGuid);
                }
                else
                {
                    ChildModule(query.Items, result, null);
                }
                
                query.Items = result;
                res.data = query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return res;
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
        public async Task<ApiResult<string>> ModifyAsync(SysMenu parm, List<string> btnfun)
        {
            var res = new ApiResult<string>
            {
                statusCode = 200
            };
            //判断别名是否存在，要不一样的
            var isCodeExis = SysMenuDb.GetSingle(m => m.NameCode == parm.NameCode && m.Guid!=parm.Guid);
            if (isCodeExis != null)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = "别名已存在~";
                return await Task.Run(() => res);
            }
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
            parm.BtnFunJson = JsonConvert.SerializeObject(btnfun);
            await Db.Updateable(parm).ExecuteCommandAsync();
            return res;
        }

        /// <summary>
        /// 获得菜单列表，提供给权限管理，根据角色查询所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenuDto>>> GetMenuByRole(string role)
        {
            var res = new ApiResult<List<SysMenuDto>>();
            try
            {
                res.data = await Db.Queryable<SysMenu>()                        
                        .OrderBy(m => m.Sort)
                        .Select(m=>new SysMenuDto() {
                            guid=m.Guid,
                            parentGuid=m.ParentGuid,
                            name=m.Name,
                            layer=m.Layer,
                            icon=m.Icon,
                            btnJson=m.BtnFunJson
                        })
                        .Mapper((it, cache) => {
                            //根据角色查询已授权的选项
                            var codeList = cache.Get(t =>
                            {
                                return Db.Queryable<SysCode>().Where(m => m.ParentGuid == "a88fa4d3-3658-4449-8f4a-7f438964d716").ToList();
                            });
                            var menuList = cache.Get(t =>
                            {
                                return Db.Queryable<SysPermissions>().Where(m => m.RoleGuid == role && m.Types==1).ToList();
                            });
                            var list = new List<string>();
                            if (!string.IsNullOrEmpty(it.btnJson))
                            {
                                list = JsonConvert.DeserializeObject<List<string>>(it.btnJson);
                            }
                            //判断菜单权限里是否包含当前按钮权限
                            var permissionModel = menuList.Find(g => g.MenuGuid == it.guid && g.RoleGuid == role);
                            if (permissionModel!=null)
                            {
                                it.isChecked = true;
                            }
                            if (list.Count > 0)
                            {
                                var btnList = new List<SysCodeDto>();
                                //查询当前菜单里面包含的按钮权限组
                                foreach (var item in codeList.Where(g=>list.Contains(g.Guid)))
                                {
                                    var btnIsChecied = false;
                                    if (permissionModel!=null && !string.IsNullOrEmpty(permissionModel.BtnFunJson) && permissionModel.BtnFunJson.Contains(item.Guid))
                                    {
                                        btnIsChecied = true;
                                    }
                                    btnList.Add(new SysCodeDto() {
                                        guid=item.Guid,
                                        name=item.Name,
                                        status= btnIsChecied
                                    });
                                }
                                it.btnFun = btnList;
                            }
                        }).ToListAsync();
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
                Logger.Default.ProcessError((int)ApiEnum.Error, ex.Message);
            }
            return res;
        }
    }
}
