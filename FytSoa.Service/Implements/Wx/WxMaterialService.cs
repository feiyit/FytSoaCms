﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Core.Model.Wx;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using Newtonsoft.Json;

namespace FytSoa.Service.Implements
{
    /*!
    * 文件名称：Wx_material服务接口实现
    */
    public class WxMaterialService : BaseService<WxMaterial>, IWxMaterialService
    {
        /// <summary>
        /// 添加一条公众号素材信息
        /// </summary>
        /// <param name="WxId">微信ID</param>
        /// <param name="list">内容列表</param>
        /// <returns></returns>
        public async Task<ApiResult<string>> Add(WxMaterial model, List<Material> list)
        {
            var res = new ApiResult<string>();
            try
            {
                var mList = JsonConvert.DeserializeObject<List<Material>>(model.TestJson);
                var scModel = mList[0];
                model.Title = scModel.title;
                model.Author = scModel.author;
                model.Img = scModel.img;
                model.Summary = scModel.summary;
                model.Link = scModel.link;
                model.Content = scModel.content;
                model.AddDate = DateTime.Now;
                var dbres = 0;
                if (model.Id==0)
                {
                    dbres =await Db.Insertable<WxMaterial>(model).ExecuteCommandAsync();
                }
                else
                {
                    dbres = await Db.Updateable<WxMaterial>(model).ExecuteCommandAsync();
                }
                if (dbres >1)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "执行失败~";
                }
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return res;
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="parm">参数  key=关键字  id=公众号  types=保存区域</param>
        /// <returns></returns>
        public async Task<ApiResult<Page<WxMaterial>>> GetPageList(PageParm parm)
        {
            var res = new ApiResult<Page<WxMaterial>>();
            try
            {
                res.data = await Db.Queryable<WxMaterial>()
                        .WhereIF(!string.IsNullOrEmpty(parm.key), m => m.Title.Contains(parm.key))
                        .WhereIF(parm.types!=0, m => m.Position==parm.types)
                        .WhereIF(parm.id != 0, m => m.WxId == parm.id)
                        .OrderBy(m => m.AddDate).ToPageAsync(parm.page, parm.limit);
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return res;
        }
    }
}