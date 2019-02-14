using FytSoa.Core.Model.Cms;
using FytSoa.Core.Model.Sys;
using FytSoa.Extensions;
using SqlSugar;

namespace FytSoa.Core
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigExtensions.Configuration["DbConnection:MySqlConnectionString"],
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                string s = sql;
                //Console.WriteLine(sql + "\r\n" +
                //    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                //Console.WriteLine();
            };
        }
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public DbSet<DbModel> GetDb<DbModel>() where DbModel : class, new()
        {
            return new DbSet<DbModel>(Db);
        }

        //Cms设置
        public DbSet<CmsImage> CmsImageDb => new DbSet<CmsImage>(Db);
        public DbSet<CmsAdvClass> CmsAdvClassDb => new DbSet<CmsAdvClass>(Db);
        public DbSet<CmsAdvList> CmsAdvListDb => new DbSet<CmsAdvList>(Db);
        public DbSet<CmsArticle> CmsArticleDb => new DbSet<CmsArticle>(Db);
        public DbSet<CmsColumn> CmsColumnDb => new DbSet<CmsColumn>(Db);
        public DbSet<CmsComment> CmsCommentDb => new DbSet<CmsComment>(Db);
        public DbSet<CmsDownload> CmsDownloadDb => new DbSet<CmsDownload>(Db);
        public DbSet<CmsSite> CmsSiteDb => new DbSet<CmsSite>(Db);
        public DbSet<CmsTags> CmsTagsDb => new DbSet<CmsTags>(Db);
        public DbSet<CmsTemplate> CmsTemplateDb => new DbSet<CmsTemplate>(Db);
        public DbSet<CmsVote> CmsVoteDb => new DbSet<CmsVote>(Db);
        public DbSet<CmsVoteItem> CmsVoteItemDb => new DbSet<CmsVoteItem>(Db);
        public DbSet<CmsVoteLog> CmsVoteLogDb => new DbSet<CmsVoteLog>(Db);

        //系统权限设置
        public DbSet<SysCode> SysCodeDb => new DbSet<SysCode>(Db);
        public DbSet<SysCodeType> SysCodeTypeDb => new DbSet<SysCodeType>(Db);
        public DbSet<SysOrganize> SysOrganizeDb => new DbSet<SysOrganize>(Db);
        public DbSet<SysLog> SysLogDb => new DbSet<SysLog>(Db);
        public DbSet<SysMenu> SysMenuDb => new DbSet<SysMenu>(Db);
        public DbSet<SysPermissions> SysPermissionsDb => new DbSet<SysPermissions>(Db);
        public DbSet<SysRole> SysRoleDb => new DbSet<SysRole>(Db);
        public DbSet<SysAdmin> SysAdminDb => new DbSet<SysAdmin>(Db);
        public DbSet<SysBtnFun> SysBtnFunDb => new DbSet<SysBtnFun>(Db);
        public DbSet<SysAppSetting> SysAppSettingDb => new DbSet<SysAppSetting>(Db);

    }
    /// <summary>
    /// 扩展ORM
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context)
        {

        }
        /// <summary>
        /// 扩展假删除功能
        /// </summary>
        /// <typeparam name="DbModel"></typeparam>
        /// <param name="dbModel"></param>
        /// <returns></returns>
        public bool FalseDelete<DbModel>(DbModel dbModel) where DbModel : BaseDbModel, new()
        {
            return this.Context.Updateable<DbModel>(dbModel).UpdateColumns(it => new DbModel() { IsDel = true }).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 扩展假删除功能
        /// </summary>
        /// <typeparam name="DbModel"></typeparam>
        /// <param name="dbModel"></param>
        /// <returns></returns>
        public bool FalseDelete<DbModel>(DbModel[] dbModels) where DbModel : BaseDbModel, new()
        {
            return this.Context.Updateable<DbModel>(dbModels).UpdateColumns(it => new DbModel() { IsDel = true }).ExecuteCommand() > 0;
        }
    }
}
