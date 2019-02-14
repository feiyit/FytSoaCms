using FytSoa.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FytSoa.Core
{
    public class DbBackup
    {
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="path">备份文件地址如D://abc.sql</param>
        /// <returns></returns>
        public static void BackupDb(object path)
        {
            bool isSuccess = false;
            try
            {
                MySqlConnection myconn = new MySqlConnection(ConfigExtensions.Configuration["DbConnection:MySqlConnectionString"]);
                if (myconn.State == ConnectionState.Closed)
                {
                    myconn.Open();
                }
                try
                {

                    using (MySqlCommand cmmd = new MySqlCommand())
                    {
                        using (MySqlBackup backCmd = new MySqlBackup(cmmd))
                        {
                            cmmd.Connection = myconn;
                            cmmd.CommandTimeout = 60;
                            backCmd.ExportInfo.MaxSqlLength = 2048;//指定备份文件的大小
                            backCmd.ExportToFile(path.ToString());
                            isSuccess = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"BackupDB_备份数据库异常 sql:{cmdText}. {ex.Message}", "MYSQLIMPL");
                }
                finally
                {
                    if (myconn.State == ConnectionState.Open)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Error($"BackupDB_备份数据库异常。ex.Message}", "MYSQLIMPL");
            }
            //return isSuccess;
        }

        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="path">指定还原文件***.sql的绝对路径</param>
        /// <param name="dbName">还原到指定数据库</param>
        /// <returns></returns>
        public static bool RestoreDb(string path, string dbName)
        {
            bool isSuccess = false;
            try
            {
                MySqlConnection myconn = new MySqlConnection(ConfigExtensions.Configuration["DbConnection:MySqlConnectionString"]);
                if (myconn.State == ConnectionState.Closed)
                {
                    myconn.Open();
                }
                try
                {

                    using (MySqlCommand cmmd = new MySqlCommand())
                    {
                        using (MySqlBackup backCmd = new MySqlBackup(cmmd))
                        {
                            cmmd.Connection = myconn;
                            cmmd.CommandTimeout = 3600;
                            backCmd.ImportInfo.TargetDatabase = dbName;//前提条件 当前 myconn 中的用户有建库等系列权限
                            backCmd.ImportInfo.DatabaseDefaultCharSet = "utf8";
                            backCmd.ImportFromFile(path);
                            isSuccess = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"BackupDB_备份数据库异常 sql:{cmdText}. {ex.Message}", "MYSQLIMPL");
                }
                finally
                {
                    if (myconn.State == ConnectionState.Open)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
            }
            catch (Exception)
            {

            }
            return isSuccess;
        }
    }
}
