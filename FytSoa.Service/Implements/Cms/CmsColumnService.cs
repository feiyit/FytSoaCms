using FytSoa.Common;
using FytSoa.Core.Model.Cms;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /*!
    * 文件名称：CmsColumn服务接口实现
    * 版权所有：北京飞易腾科技有限公司
    * 企业官网：http://www.feiyit.com
    */
    public class CmsColumnService : BaseServer<CmsColumn>, ICmsColumnService
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public new async Task<ApiResult<string>> AddAsync(CmsColumn parm)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                parm.Number = Utils.Number(10);
                //根据模板ID查询相关内容
                var mbModel = CmsTemplateDb.GetSingle(m => m.Id == parm.TempId);
                if (mbModel != null)
                {
                    parm.TempName = mbModel.Title;
                    parm.TempUrl = mbModel.Url;
                }
                //如果描述不写，直接读取内容
                if (!string.IsNullOrEmpty(parm.Summary))
                {
                    parm.Summary = Utils.CutString(parm.Content, 160);
                }
                //生成排序数字
                var sorts = Db.Queryable<CmsColumn>().OrderBy(m => m.Sort, SqlSugar.OrderByType.Desc).Take(1).First();
                if (sorts != null)
                {
                    parm.Sort = sorts.Sort + 1;
                }
                else
                {
                    parm.Sort = 1;
                }
                try
                {
                    var result = Db.Ado.UseTran(() =>
                    {
                        var addId = Db.Insertable(parm).ExecuteReturnIdentity();
                        if (parm.ParentId > 0)
                        {
                            //说明有父级  根据父级，查询对应的模型
                            var parModel = Db.Queryable<CmsColumn>().Single(m => m.Id == parm.ParentId);
                            if (parModel != null)
                            {
                                parm.ClassList = parModel.ClassList + addId + ",";
                                parm.ClassLayer = parModel.ClassLayer + 1;
                                parm.Id = addId;
                            }
                        }
                        else
                        {
                            //没有父级
                            parm.ClassList = "," + addId + ",";
                        }
                        Db.Updateable(parm).ExecuteCommand();
                    });
                    if (result.IsSuccess)
                    {
                        res.statusCode = (int)ApiEnum.Status;
                    }
                    else
                    {
                        res.message = result.ErrorMessage;
                    }

                }
                catch (Exception ex)
                {
                    res.message = ex.Message;
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="parm">T</param>
        /// <returns></returns>
        public new async Task<ApiResult<string>> UpdateAsync(CmsColumn parm)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                //先查出原来的
                var sourceModel = CmsColumnDb.GetSingle(m => m.Id == parm.Id);
                //根据模板ID查询相关内容
                var mbModel = CmsTemplateDb.GetSingle(m => m.Id == parm.TempId);
                if (parm.TempId != sourceModel.TempId && mbModel != null)
                {
                    parm.TempName = mbModel.Title;
                    parm.TempUrl = mbModel.Url;
                }
                else
                {
                    parm.TempName = sourceModel.TempName;
                    parm.TempUrl = sourceModel.TempUrl;
                }
                if (sourceModel.ParentId != parm.ParentId)
                {
                    //不相等更改等级
                    var parModel = CmsColumnDb.GetSingle(m => m.Id == parm.ParentId);
                    if (parModel != null)
                    {
                        parm.ClassList = parModel.ClassList + parm.Id + ",";
                        parm.ClassLayer = parModel.ClassLayer + 1;
                    }
                }
                else
                {
                    parm.ClassList = sourceModel.ClassList;
                    parm.ClassLayer = sourceModel.ClassLayer;
                }
                Db.Updateable(parm).IgnoreColumns(m => new { m.Number, m.Sort, }).ExecuteCommand();
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="p">父级</param>
        /// <param name="i">当前id</param>
        /// <param name="o">排序方式</param>
        /// <returns></returns>
        public async Task<ApiResult<string>> ColSort(int p, int i, int o)
        {
            var res = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                int a = 0, b = 0, c = 0;
                var list = CmsColumnDb.GetList(m => m.ParentId == p).OrderBy(m => m.Sort).ToList();
                if (list.Count > 0)
                {
                    var index = 0;
                    foreach (var item in list)
                    {
                        index++;
                        if (index == 1)
                        {
                            if (item.Id == i) //判断是否是头如果上升则不做处理
                            {
                                if (o == 1) //下降一位
                                {
                                    a = Convert.ToInt32(item.Sort);
                                    b = Convert.ToInt32(list[index].Sort);
                                    c = a;
                                    a = b;
                                    b = c;
                                    item.Sort = a;
                                    CmsColumnDb.Update(item);
                                    var nitem = list[index];
                                    nitem.Sort = b;
                                    CmsColumnDb.Update(nitem);
                                    break;
                                }
                            }
                        }
                        else if (index == list.Count)
                        {
                            if (item.Id == i) //最后一条如果下降则不做处理
                            {
                                if (o == 0) //上升一位
                                {
                                    a = Convert.ToInt32(item.Sort);
                                    b = Convert.ToInt32(list[index - 2].Sort);
                                    c = a;
                                    a = b;
                                    b = c;
                                    item.Sort = a;
                                    CmsColumnDb.Update(item);
                                    var nitem = list[index - 2];
                                    nitem.Sort = b;
                                    CmsColumnDb.Update(nitem);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (item.Id == i) //判断是否是头如果上升则不做处理
                            {
                                if (o == 1) //下降一位
                                {
                                    a = Convert.ToInt32(item.Sort);
                                    b = Convert.ToInt32(list[index].Sort);
                                    c = a;
                                    a = b;
                                    b = c;
                                    item.Sort = a;
                                    CmsColumnDb.Update(item);
                                    var nitem = list[index];
                                    nitem.Sort = b;
                                    CmsColumnDb.Update(nitem);
                                    break;
                                }
                                else
                                {
                                    a = Convert.ToInt32(item.Sort);
                                    b = Convert.ToInt32(list[index - 2].Sort);
                                    c = a;
                                    a = b;
                                    b = c;
                                    item.Sort = a;
                                    CmsColumnDb.Update(item);
                                    var nitem = list[index - 2];
                                    nitem.Sort = b;
                                    CmsColumnDb.Update(nitem);
                                    break;
                                }
                            }
                        }
                    }
                }
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 反向递归模块集合，可重复模块数据，最后去重
        /// </summary>
        /// <param name="prevModule">总模块</param>
        /// <param name="retmodule">返回模块</param>
        /// <param name="parentId">上级ID</param>
        private void RecursiveModule(List<CmsColumn> prevModule, List<CmsColumn> retmodule, int? parentId)
        {
            var result = prevModule.Where(p => p.Id == parentId);
            foreach (var item in result)
            {
                retmodule.Add(item);
                RecursiveModule(prevModule, retmodule, item.ParentId);
            }
        }

        /// <summary>
        /// 递归模块列表，返回按级别排序
        /// </summary>
	    public List<CmsColumn> RecursiveModule(List<CmsColumn> list)
        {
            List<CmsColumn> result = new List<CmsColumn>();
            if (list != null && list.Count > 0)
            {
                ChildModule(list, result, 0);
            }
            return result;
        }
        /// <summary>
        /// 递归模块列表
        /// </summary>
        private void ChildModule(List<CmsColumn> list, List<CmsColumn> newlist, int parentId)
        {
            var result = list.Where(p => p.ParentId == parentId).OrderBy(p => p.ClassLayer).ThenBy(p => p.Sort).ToList();
            if (result.Any())
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    newlist.Add(result[i]);
                    ChildModule(list, newlist, result[i].Id);
                }
            }
        }

        /// <summary>
        /// 栏目Tree
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ColumnTree>>> TreeAsync(int type = 1)
        {
            var res = new ApiResult<List<ColumnTree>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var list = Db.Queryable<CmsColumn>()
                    .Where(m => m.TypeID == type)
                    .Select(m => new ColumnTree()
                    {
                        Id = m.Id,
                        ColumnId = m.ParentId,
                        Name = m.Title,
                        Href = m.TempUrl,
                        TempId = m.TempId,
                        Sort = m.ClassLayer
                    }).ToList();

                var resList = new List<ColumnTree>();
                foreach (var item in list.Where(m => m.ColumnId == 0).OrderBy(m => m.Sort))
                {
                    resList.Add(new ColumnTree()
                    {
                        Id = item.Id,
                        ColumnId = item.ColumnId,
                        Name = item.Name,
                        Href = item.Href + item.Id,
                        TempId = item.TempId,
                        Sort = item.Sort,
                        children = RecursiveModule(list, item)
                    });
                }
                res.data = resList;
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.message = ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 递归模块列表，返回按级别排序
        /// </summary>
	    public List<ColumnTree> RecursiveModule(List<ColumnTree> list, ColumnTree model)
        {
            var nodeList = new List<ColumnTree>();
            var children = list.Where(t => t.ColumnId == model.Id).OrderBy(m => m.Sort);
            if (children.Any())
            {
                foreach (var item in children)
                {
                    nodeList.Add(new ColumnTree()
                    {
                        Id = item.Id,
                        ColumnId = item.ColumnId,
                        Name = item.Name,
                        Href = item.Href + item.Id,
                        TempId = item.TempId,
                        Sort = item.Sort,
                        children = RecursiveModule(list, item)
                    });
                }
            }
            return nodeList;
        }


        /// <summary>
        /// 模型去重，非常重要
        /// </summary>
        public class ModuleDistinct : IEqualityComparer<CmsColumn>
        {
            public bool Equals(CmsColumn x, CmsColumn y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(CmsColumn obj)
            {
                return obj.ToString().GetHashCode();
            }
        }
    }
}