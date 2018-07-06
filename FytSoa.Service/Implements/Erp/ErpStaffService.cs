using FytIms.Service.Extensions;
using FytSoa.Common;
using FytSoa.Core;
using FytSoa.Core.Model.Erp;
using FytSoa.Service.DtoModel;
using FytSoa.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Service.Implements
{
    /// <summary>
    /// 员工接口实现
    /// </summary>
    public class ErpStaffService : DbContext, IErpStaffService
    {
        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyLoginPwdAsync(StaffModifyPwdParm parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = (int)ApiEnum.ParameterError };
            try
            {
                //判断原密码是否正确
                parm.HistoryPwd = DES3Encrypt.EncryptString(parm.HistoryPwd);
                var isExt = ErpStaffDb.IsAny(m => m.LoginPwd == parm.HistoryPwd && m.Guid == parm.Guid);
                if (!isExt)
                {
                    res.message = "原密码输入错误~";
                    return await Task.Run(() => res);
                }
                //开始修改新密码
                parm.NewPwd = DES3Encrypt.EncryptString(parm.NewPwd);
                ErpStaffDb.Update(m=>new ErpStaff() {
                    LoginPwd=parm.NewPwd
                },m=>m.Guid==parm.Guid);
                res.statusCode = (int)ApiEnum.Status;
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 根据账号密码登录
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ShopBasicDto>> LoginAsync(StaffLoginDto parm)
        {
            var res = new ApiResult<ShopBasicDto> { statusCode = (int)ApiEnum.ParameterError };
            //先查询员工账号是否可以登录，如果可以，根据员工的归属加盟商，在查询对应的加盟商
            var staffModel = ErpStaffDb.GetSingle(m => m.LoginName == parm.loginName);
            if (staffModel == null)
            {
                res.message = "登录账号错误";
                return await Task.Run(() => res);
            }
            //判断密码
            parm.loginPwd = DES3Encrypt.EncryptString(parm.loginPwd);
            if (staffModel.LoginPwd != parm.loginPwd)
            {
                res.message = "密码错误";
                return await Task.Run(() => res);
            }
            if (staffModel.Status != 0)
            {
                res.message = "账号被冻结，请联系管理员";
                return await Task.Run(() => res);
            }
            //根据商铺ID查询店铺信息，以及修改账号登录信息
            staffModel.LoginCount += 1;
            staffModel.LastLoginDate = DateTime.Now;
            staffModel.UpLoginDate = staffModel.LastLoginDate;
            staffModel.IsDevice = parm.isDevice;
            staffModel.DeviceName = parm.deviceName;
            staffModel.Token = parm.token;
            //修改员工信息
            ErpStaffDb.Update(staffModel);

            //根据员工的关联的加盟商ID，查询加盟商信息
            var shopModel = ErpShopsDb.GetSingle(m => m.Guid == staffModel.ShopGuid);
            if (shopModel == null)
            {
                res.message = "店铺不存在！";
                return await Task.Run(() => res);
            }
            res.statusCode = (int)ApiEnum.Status;
            res.data = new ShopBasicDto()
            {
                StaffGuid=staffModel.Guid,
                ShopGuid = shopModel.Guid,
                ShopName = shopModel.ShopName,
                AdminName = staffModel.TrueName,
                Mobile=staffModel.Mobile
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AddAsync(ErpStaff parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                //判断登录账号
                var isExt = ErpStaffDb.IsAny(m => m.LoginName == parm.LoginName);
                if (isExt)
                {
                    res.statusCode = (int)ApiEnum.ParameterError;
                    res.message = "该登录账号已存在~";
                }
                else
                {
                    parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                    parm.Guid = Guid.NewGuid().ToString();
                    var dbres = ErpStaffDb.Insert(parm);
                    if (!dbres)
                    {
                        res.statusCode = (int)ApiEnum.Error;
                        res.message = "插入数据失败~";
                    }
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 删除一条或多条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAsync(string parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                var list = Utils.StrToListString(parm);
                var dbres = ErpStaffDb.Delete(m => list.Contains(m.Guid));
                if (!dbres)
                {
                    res.statusCode = (int)ApiEnum.Error;
                    res.message = "删除数据失败~";
                }
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 获得一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ErpStaff>> GetByGuidAsync(string parm)
        {
            var model = ErpStaffDb.GetById(parm);
            var res = new ApiResult<ErpStaff>
            {
                statusCode = 200,
                data = model ?? new ErpStaff() { }
            };
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult<Page<ErpStaff>>> GetPagesAsync(PageParm parm)
        {
            var res = new ApiResult<Page<ErpStaff>>();
            try
            {
                string beginTime = string.Empty, endTime = string.Empty;
                if (!string.IsNullOrEmpty(parm.time))
                {
                    var timeRes = Utils.SplitString(parm.time, '-');
                    beginTime = timeRes[0].Trim();
                    endTime = timeRes[1].Trim();
                }
                var query = Db.Queryable<ErpStaff>()
                        .WhereIF(!string.IsNullOrEmpty(parm.guid),m=>m.ShopGuid==parm.guid)
                        .WhereIF(!string.IsNullOrEmpty(parm.key),
                        m => m.LoginName == parm.key
                        || m.TrueName == parm.key
                        || m.Mobile == parm.key)
                        .WhereIF(!string.IsNullOrEmpty(parm.time), m => m.AddDate >= Convert.ToDateTime(beginTime) && m.AddDate <= Convert.ToDateTime(endTime))
                        .OrderBy(m => m.AddDate,OrderByType.Desc).ToPageAsync(parm.page, parm.limit);
                res.success = true;
                res.message = "获取成功！";
                res.data = await query;
            }
            catch (Exception ex)
            {
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
                res.statusCode = (int)ApiEnum.Error;
            }
            return await Task.Run(() => res);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> ModifyAsync(ErpStaff parm)
        {
            var res = new ApiResult<string>() { data = "1", statusCode = 200 };
            try
            {
                parm.LoginPwd = DES3Encrypt.EncryptString(parm.LoginPwd);
                ErpStaffDb.Update(m=>new ErpStaff(){
                    TrueName=parm.TrueName,
                    Sex=parm.Sex,
                    Mobile=parm.Mobile,
                    LoginPwd=parm.LoginPwd
                },m=>m.Guid==parm.Guid);
            }
            catch (Exception ex)
            {
                res.statusCode = (int)ApiEnum.Error;
                res.message = ApiEnum.Error.GetEnumText() + ex.Message;
            }
            return await Task.Run(() => res);
        }
    }
}
