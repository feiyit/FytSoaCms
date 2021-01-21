using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using FytSoa.Tasks;
using FytSoa.Common;
using Quartz;
using FytSoa.Extensions;

namespace FytSoa.Api.Controllers.Tasks
{
    [Route("api/[controller]/[action]")]
    [JwtAuthorize(Roles = "Admin")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ITaskSchedulingService _scheduler;

        /// <summary>
        /// 任务调度对象
        /// </summary>
        /// <param name="schedulerCenter"></param>
        public JobController(ITaskSchedulingService schedulerCenter)
        {
            this._scheduler = schedulerCenter;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AddJob([FromBody]ScheduleEntity entity)
        {
            return await _scheduler.Add(entity);
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> StopJob([FromBody]JobKey job)
        {
            return await _scheduler.Stop(job);
        }

        /// <summary>
        /// 恢复运行暂停的任务
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> ResumeJob([FromBody]JobKey job)
        {
            return await _scheduler.Recovery(job);
        }

        /// <summary>
        /// 删除任务
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> RemoveJob([FromBody]JobKey job)
        {
            return await _scheduler.Delete(job);
        }

        /// <summary>
        /// 查询任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<ScheduleEntity> QueryJob([FromBody] ScheduleEntity job)
        {
            if (string.IsNullOrEmpty(job.JobGroup) || string.IsNullOrEmpty(job.JobName))
            {
                return new ApiResult<ScheduleEntity>() { data=new ScheduleEntity() { }  };
            }
            //直接到Redis里面读取
            var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
            return new ApiResult<ScheduleEntity>() { data=redisTask.FirstOrDefault(m=>m.JobGroup==job.JobGroup && m.JobName==job.JobName) };
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> ModifyJob([FromBody]ScheduleEntity entity)
        {
            return await _scheduler.Modify(entity);
        }

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> TriggerJob([FromBody]JobKey job)
        {
            return await _scheduler.Execute(job);
        }

        /// <summary>
        /// 启动调度
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<bool> StartSchedule()
        //{
        //    return await _scheduler.StartScheduleAsync();
        //}

        /// <summary>
        /// 停止调度
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<bool> StopSchedule()
        //{
        //    return await _scheduler.StopScheduleAsync();
        //}

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<JobListEntity>>> GetAllJob()
        {
            return await _scheduler.GetList();
        }

        /// <summary>
        /// 获取所有Job信息（简要信息 - 刷新数据的时候使用）
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<List<JobBriefInfoEntity>> GetAllJobBriefInfo()
        //{
        //    return await _scheduler.GetAllJobBriefInfoAsync();
        //}

        /// <summary>
        /// 获取job日志
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<string>>> GetJobLogs([FromBody]JobKey jobKey)
        {
            return await _scheduler.JobLogs(jobKey);
        }

    }
}
