using FytSoa.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Tasks
{
    /// <summary>
    /// 任务调度接口——基于Redis存储
    /// </summary>
    public interface ITaskSchedulingService
    {
        /// <summary>
        /// 查询所有任务
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<JobListEntity>>> GetList();

        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> Add(ScheduleEntity model);

        /// <summary>
        /// 修改一个任务
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> Modify(ScheduleEntity model);

        /// <summary>
        /// 删除一个任务
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> Delete(JobKey job);

        /// <summary>
        /// 停止一个任务
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> Stop(JobKey job);

        /// <summary>
        /// 恢复一个任务
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> Recovery(JobKey job);

        /// <summary>
        /// 手动执行一个任务
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> Execute(JobKey job);

        /// <summary>
        /// 根据任务查询运行日志
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<string>>> JobLogs(JobKey jobKey);
    }
}
