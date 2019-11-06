using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using FytSoa.Tasks;
using FytSoa.Common;
using Quartz;

namespace FytSoa.Api.Controllers.Tasks
{
    [Route("api/[controller]/[action]")]
    [EnableCors("Any")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly SchedulerCenter _scheduler;

        /// <summary>
        /// 任务调度对象
        /// </summary>
        /// <param name="schedulerCenter"></param>
        public JobController(SchedulerCenter schedulerCenter)
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
            var res = await _scheduler.AddScheduleJobAsync(entity);
            if (res.statusCode==200)
            {
                var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                if (redisTask==null)
                {
                    //实例数组
                    redisTask = new List<ScheduleEntity>();
                }
                redisTask.Add(entity);
                //保存到Redis中
                RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
            }
            return res;
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> StopJob([FromBody]JobKey job)
        {
            var res= await _scheduler.StopOrDelScheduleJobAsync(job.Group, job.Name);
            if (res.statusCode==200)
            {
                //更改Redis中的状态
                var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                if (redisTask != null && redisTask.Count > 0)
                {
                    redisTask.FirstOrDefault(m => m.JobGroup == job.Group && m.JobName == job.Name).TriggerState = TriggerState.Paused;
                    //保存到Redis中
                    RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
                }
            }
            return res;
        }

        /// <summary>
        /// 恢复运行暂停的任务
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> ResumeJob([FromBody]JobKey job)
        {
            var res = await _scheduler.ResumeJobAsync(job.Group, job.Name);
            if (res.statusCode==200)
            {
                //更改Redis中的状态
                var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                if (redisTask != null && redisTask.Count > 0)
                {
                    redisTask.FirstOrDefault(m => m.JobGroup == job.Group && m.JobName == job.Name).TriggerState = TriggerState.Normal;
                    //保存到Redis中
                    RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
                }
            }
            return res;
        }

        /// <summary>
        /// 删除任务
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> RemoveJob([FromBody]JobKey job)
        {
            //删除Redis里面的任务
            var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
            if (redisTask!=null && redisTask.Count>0)
            {
                var delModel = redisTask.FirstOrDefault(m => m.JobGroup == job.Group && m.JobName == job.Name);
                redisTask.Remove(delModel);
                //保存到Redis中
                RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
            }
            return await _scheduler.StopOrDelScheduleJobAsync(job.Group, job.Name, true);
        }

        /// <summary>
        /// 查询任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<ScheduleEntity> QueryJob([FromBody]ScheduleEntity job)
        {
            //return await _scheduler.QueryJobAsync(job.Group, job.Name);
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
            await _scheduler.StopOrDelScheduleJobAsync(entity.JobGroup, entity.JobName, true);
            //删除Redis里面的任务
            var redisTask= RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
            if (redisTask != null && redisTask.Count > 0)
            {
                var delModel = redisTask.FirstOrDefault(m => m.JobGroup == entity.JobGroup && m.JobName == entity.JobName);
                redisTask.Remove(delModel);
            }
            var res= await _scheduler.AddScheduleJobAsync(entity);
            if (res.statusCode==200)
            {
                //添加新的任务
                redisTask.Add(entity);
                //保存到Redis中
                RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
            }
            return res;
        }

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> TriggerJob([FromBody]JobKey job)
        {
            return await _scheduler.TriggerJobAsync(job);
        }

        /// <summary>
        /// 启动调度
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> StartSchedule()
        {
            return await _scheduler.StartScheduleAsync();
        }

        /// <summary>
        /// 停止调度
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> StopSchedule()
        {
            return await _scheduler.StopScheduleAsync();
        }

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<JobInfoEntity>>> GetAllJob()
        {
            var res = await _scheduler.GetAllJobAsync();
            if (res.data!=null && res.data.Count>0)
            {
                return res;
            }
            //查询redis
            var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
            if (redisTask != null && redisTask.Count > 0)
            {
                //添加任务里面
                foreach (var item in redisTask)
                {
                    await _scheduler.AddScheduleJobAsync(item);
                }
            }
            //重新读取
            return await _scheduler.GetAllJobAsync();
        }

        /// <summary>
        /// 获取所有Job信息（简要信息 - 刷新数据的时候使用）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<JobBriefInfoEntity>> GetAllJobBriefInfo()
        {
            return await _scheduler.GetAllJobBriefInfoAsync();
        }

        /// <summary>
        /// 获取job日志
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<string>>> GetJobLogs([FromBody]JobKey jobKey)
        {
            return await _scheduler.GetJobLogsAsync(jobKey);
        }

    }
}
