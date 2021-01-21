using FytSoa.Common;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Tasks
{
    /// <summary>
    /// 任务调度接口——基于Redis存储
    /// </summary>
    public class TaskSchedulingService : ITaskSchedulingService
    {
        /// <summary>
        /// 任务调度对象
        /// </summary>
        public static readonly TaskSchedulingService Instance;

        static TaskSchedulingService()
        {
            Instance = new TaskSchedulingService();
        }

        private static IScheduler _scheduler;
        /// <summary>
        /// 返回任务计划（调度器）
        /// </summary>
        /// <returns></returns>
        private IScheduler Scheduler
        {
            get
            {
                if (_scheduler != null)
                {
                    return _scheduler;
                }

                #region 如果redis不需要数据库则启用这个
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                _scheduler = schedulerFactory.GetScheduler().Result;
                _scheduler.Start();//默认开始调度器
                return _scheduler;
                #endregion
            }
        }

        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> Add(ScheduleEntity entity)
        {
            var result = new ApiResult<string>() {statusCode=(int)ApiEnum.Error };
            try
            {
                //检查任务是否已存在
                var jobKey = new JobKey(entity.JobName, entity.JobGroup);
                if (await Scheduler.CheckExists(jobKey))
                {
                    result.message = "任务已存在";
                    return result;
                }
                //http请求配置
                var httpDir = new Dictionary<string, string>()
                {
                    { "RequestUrl",entity.RequestUrl},
                    { "RequestParameters",entity.RequestParameters},
                    { "RequestType", ((int)entity.RequestType).ToString()},
                    { Constant.HEADERS, entity.Headers},
                    { Constant.MAILMESSAGE, ((int)entity.MailMessage).ToString()},
                };
                // 定义这个工作，并将其绑定到我们的IJob实现类                
                IJobDetail job = JobBuilder.CreateForAsync<HttpJob>()
                    .SetJobData(new JobDataMap(httpDir))
                    .WithDescription(entity.Description)
                    .WithIdentity(entity.JobName, entity.JobGroup)
                    .Build();
                // 创建触发器
                ITrigger trigger;
                //校验是否正确的执行周期表达式
                if (entity.TriggerType == TriggerTypeEnum.Cron)//CronExpression.IsValidExpression(entity.Cron))
                {
                    trigger = CreateCronTrigger(entity);
                }
                else
                {
                    trigger = CreateSimpleTrigger(entity);
                }

                // 告诉Quartz使用我们的触发器来安排作业
                await Scheduler.ScheduleJob(job, trigger);

                //将作业增加到Redis里面
                var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                if (redisTask == null)
                {
                    //实例数组
                    redisTask = new List<ScheduleEntity>();
                }
                if (!redisTask.Any(m => m.JobId == entity.JobId))
                {
                    entity.JobId = Utils.GetOrderNumber();
                    redisTask.Add(entity);
                    //保存到Redis中
                    RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
                }
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除一个任务
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> Delete(JobKey job)
        {
            var result = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                await Scheduler.DeleteJob(job);
                var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                var task = redisTask.Where(m=>m.JobGroup==job.Group && m.JobName==job.Name).FirstOrDefault();
                redisTask.Remove(task);
                //保存到Redis中
                RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 手动执行一个任务
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> Execute(JobKey job)
        {
            var result = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                await Scheduler.TriggerJob(job);
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 查询所有任务
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<JobListEntity>>> GetList()
        {
            var result = new ApiResult<List<JobListEntity>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                
                List<JobKey> jboKeyList = new List<JobKey>();
                List<JobListEntity> jobInfoList = new List<JobListEntity>();
                //查询redis的任务
                var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                if(redisTask==null || redisTask.Count == 0)
                {
                    result.statusCode = (int)ApiEnum.Status;
                    result.data = jobInfoList;
                    return result;
                }

                var groupNames = await Scheduler.GetJobGroupNames();
                if (groupNames.Count == 0)
                {
                    //说明第一次打开，增加到任务调度里面
                    foreach (var item in redisTask)
                    {
                        if(item.TriggerState== TriggerState.Normal)
                        {
                            await Add(item);
                        }
                    }
                    //阻塞下
                    System.Threading.Thread.Sleep(500);
                    //阻塞后，重新获得在任务中的组信息
                    groupNames = await Scheduler.GetJobGroupNames();
                }
                
                foreach (var groupName in groupNames.OrderBy(t => t))
                {
                    jboKeyList.AddRange(await Scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName)));
                    jobInfoList.Add(new JobListEntity() { GroupName = groupName });
                }
                //定义一个空数组，接受已存在任务里面的任务名字，提供下面，判断是否存在
                var jobNameArr = new List<string>();
                foreach (var jobKey in jboKeyList.OrderBy(t => t.Name))
                {
                    var jobDetail = await Scheduler.GetJobDetail(jobKey);
                    var triggersList = await Scheduler.GetTriggersOfJob(jobKey);
                    var triggers = triggersList.AsEnumerable().FirstOrDefault();
                    foreach (var jobInfo in jobInfoList)
                    {
                        if (jobInfo.GroupName == jobKey.Group)
                        {
                            //在redis里面查询任务
                            var cacheTask = redisTask.Where(m=>m.JobName== jobKey.Name && m.JobGroup==jobKey.Group).FirstOrDefault();
                            if (cacheTask!=null)
                            {
                                jobNameArr.Add(cacheTask.JobName);
                                cacheTask.LastErrMsg = jobDetail.JobDataMap.GetString(Constant.EXCEPTION);
                                cacheTask.PreviousFireTime = triggers.GetPreviousFireTimeUtc()?.LocalDateTime;
                                cacheTask.NextFireTime = triggers.GetNextFireTimeUtc()?.LocalDateTime;
                                jobInfo.jobList.Add(cacheTask);
                            }
                        }
                    }
                }
                //查询Redis中，不在运行的任务项目
                var noTaskList = redisTask.Where(m => !jobNameArr.Contains(m.JobName)).ToList();
                //将不运行的任务，增加到List中
                if (noTaskList.Any())
                {
                    //查询组
                    var noRunTaskGroup = noTaskList.GroupBy(m=>m.JobGroup).Select(m=>m.Key).ToList();
                    if(noRunTaskGroup!=null && noRunTaskGroup.Count > 0)
                    {
                        foreach (var item in noRunTaskGroup)
                        {
                            //根据组获得任务详细信息
                            var jobList = new List<ScheduleEntity>();
                            var noRunTaskList = noTaskList.Where(m=>m.JobGroup==item).ToList();
                            foreach (var job in noRunTaskList)
                            {
                                jobList.Add(job);
                            }
                            jobInfoList.Add(new JobListEntity()
                            {
                                GroupName = item,
                                jobList= jobList
                            });
                        }
                    }
                    
                }
                result.data = jobInfoList;
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改一个任务
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> Modify(ScheduleEntity model)
        {
            var result = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                var task = redisTask.Where(m => m.JobId == model.JobId).FirstOrDefault();
                if (task != null)
                {
                    //删除已存在
                    redisTask.Remove(task);
                    //保存到Redis中
                    RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
                    //新增到任务里面
                    await Add(model);
                }
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 恢复一个任务
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> Recovery(JobKey job)
        {
            var result = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                if (await Scheduler.CheckExists(job))
                {
                    //任务已经存在则暂停任务
                    await Scheduler.ResumeJob(job);
                    var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                    redisTask.Where(m => m.JobGroup == job.Group && m.JobName == job.Name).FirstOrDefault().TriggerState = TriggerState.Normal;
                    //保存到Redis中
                    RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
                }
                else
                {
                    var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                    var taskModel = redisTask.FirstOrDefault(m=>m.JobGroup==job.Group && m.JobName==job.Name);
                    //增加到任务里面
                    await Add(taskModel);
                }
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 停止一个任务
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> Stop(JobKey job)
        {
            var result = new ApiResult<string>() { statusCode = (int)ApiEnum.Error };
            try
            {
                //暂停任务
                await Scheduler.PauseJob(job);
                var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
                redisTask.Where(m => m.JobGroup == job.Group && m.JobName == job.Name).FirstOrDefault().TriggerState = TriggerState.Paused;
                //保存到Redis中
                RedisHelper.Set(KeyHelper.TaskSchedulerList, redisTask);
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据任务查询运行日志
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<string>>> JobLogs(JobKey jobKey)
        {
            var result = new ApiResult<List<string>>() { statusCode = (int)ApiEnum.Error };
            try
            {
                var jobDetail = await Scheduler.GetJobDetail(jobKey);
                result.data = jobDetail.JobDataMap[Constant.LOGLIST] as List<string>;
                result.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            return result;
        }

        public void InitStart()
        {
            var redisTask = RedisHelper.Get<List<ScheduleEntity>>(KeyHelper.TaskSchedulerList);
            if(redisTask!=null && redisTask.Count > 0)
            {
                foreach (var item in redisTask)
                {
                    if(item.TriggerState== TriggerState.Normal)
                    {
                        Task.Run(()=>Add(item));
                    }
                }
            }
        }

        /// <summary>
        /// 创建类型Cron的触发器
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ITrigger CreateCronTrigger(ScheduleEntity entity)
        {
            // 作业触发器
            return TriggerBuilder.Create()

                   .WithIdentity(entity.JobName, entity.JobGroup)
                   .StartAt(entity.BeginTime)//开始时间
                   .EndAt(entity.EndTime)//结束时间
                   .WithCronSchedule(entity.Cron, cronScheduleBuilder => cronScheduleBuilder.WithMisfireHandlingInstructionFireAndProceed())//指定cron表达式
                   .ForJob(entity.JobName, entity.JobGroup)//作业名称
                   .Build();
        }

        /// <summary>
        /// 创建类型Simple的触发器
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ITrigger CreateSimpleTrigger(ScheduleEntity entity)
        {
            //作业触发器
            if (entity.RunTimes.HasValue && entity.RunTimes > 0)
            {
                return TriggerBuilder.Create()
               .WithIdentity(entity.JobName, entity.JobGroup)
               .StartAt(entity.BeginTime)//开始时间
               .EndAt(entity.EndTime)//结束数据
               .WithSimpleSchedule(x =>
               {
                   x.WithIntervalInSeconds(entity.IntervalSecond.Value)//执行时间间隔，单位秒
                        .WithRepeatCount(entity.RunTimes.Value)//执行次数、默认从0开始
                        .WithMisfireHandlingInstructionFireNow();
               })
               .ForJob(entity.JobName, entity.JobGroup)//作业名称
               .Build();
            }
            else
            {
                return TriggerBuilder.Create()
               .WithIdentity(entity.JobName, entity.JobGroup)
               .StartAt(entity.BeginTime)//开始时间
               .EndAt(entity.EndTime)//结束数据
               .WithSimpleSchedule(x =>
               {
                   x.WithIntervalInSeconds(entity.IntervalSecond.Value)//执行时间间隔，单位秒
                        .RepeatForever()//无限循环
                        .WithMisfireHandlingInstructionFireNow();
               })
               .ForJob(entity.JobName, entity.JobGroup)//作业名称
               .Build();
            }

        }
    }
}
