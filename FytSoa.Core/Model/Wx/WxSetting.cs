using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Wx
{
    ///<summary>
    /// 公众号基本设置
    ///</summary>
    [SugarTable("Wx_setting")]
    public partial class WxSetting
    {
        public WxSetting()
        {


        }
        /// <summary>
        /// Desc:自动增长
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:公众号名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:公众平台微信号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Account { get; set; }

        /// <summary>
        /// Desc:公众号原始ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string OriginalId { get; set; }

        /// <summary>
        /// Desc:AppId
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AppId { get; set; }

        /// <summary>
        /// Desc:AppSecret
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string AppSecret { get; set; }

        /// <summary>
        /// Desc:公众号类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Type { get; set; }

        /// <summary>
        /// Desc:公众号图片
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Cover { get; set; }

        /// <summary>
        /// Desc:公众号二维码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QrCode { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool Status { get; set; } = true;

        /// <summary>
        /// Desc:自定义菜单
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string MenuJson { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime AddDate { get; set; } = DateTime.Now;

    }
}
