using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Service.DtoModel
{
    /// <summary>
    /// 接受前台设置的满减信息
    /// </summary>
    public class ShopActivityParm
    {
        /// <summary>
        /// 满多少
        /// </summary>
        public List<string> fullbegin { get; set; }

        /// <summary>
        /// 减多少
        /// </summary>
        public List<string> fullend { get; set; }
    }

    /// <summary>
    /// 构建Json保存到数据库
    /// </summary>
    public class ShopActivity
    {
        public int fullbegin { get; set; }
        public int fullend { get; set; }
    }

    /// <summary>
    /// 返回给列表显示的模型
    /// </summary>
    public class ShopActivityDto
    {
        public string Guid { get; set; }
        /// <summary>
        /// 类型(1=按商铺/2=按商品/3=按地区)
        /// </summary>
        public int Types { get; set; }
        /// <summary>
        /// 类型(1=按商铺/2=按商品/3=按地区)
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 方式(1=打折/2=满减)
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 折扣数
        /// </summary>
        public int? CountNum { get; set; }
        /// <summary>
        /// 满减
        /// </summary>
        public string FullBack { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 是否启用  1=启用  0=不启用
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 活动状态，根据开始时间和结束时间来判断
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// 提供给APP的活动内容
    /// </summary>
    public class ShopActivityApp
    {
        /// <summary>
        /// 活动编号
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 折扣值
        /// </summary>
        public int? CountNum { get; set; }
        /// <summary>
        /// 满减值
        /// </summary>
        public string FullBack { get; set; }
    }
}
