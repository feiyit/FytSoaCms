using FytSoa.Common;
using FytSoa.Core.Model.Sys;
using FytSoa.Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Interfaces
{
    /// <summary>
    /// 菜单功能接口
    /// </summary>
    public interface ISysBtnFunService : IBaseServer<SysBtnFun>
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Page<SysBtnFunDto>>> GetPagesAsync(string key,string menuKey);


        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        new Task<ApiResult<string>> AddAsync(SysBtnFun parm);

       
    }
}
