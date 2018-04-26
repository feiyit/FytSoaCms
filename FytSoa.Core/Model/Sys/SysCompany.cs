using System;
using System.Linq;
using System.Text;

namespace FytSoa.Core.Model.Sys
{
    ///<summary>
    /// 公司表
    ///</summary>
    public partial class SysCompany
    {
           public SysCompany(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Guid {get;set;}

           /// <summary>
           /// Desc:归属于集团Guid
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ParentGuid {get;set;}

           /// <summary>
           /// Desc:公司编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Codes {get;set;}

           /// <summary>
           /// Desc:层级
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Layer {get;set;}

           /// <summary>
           /// Desc:公司名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:公司Logo
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Logo {get;set;}

           /// <summary>
           /// Desc:公司法人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Corporate {get;set;}

           /// <summary>
           /// Desc:创建日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? FoundTIme {get;set;}

           /// <summary>
           /// Desc:公司文化
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Summary {get;set;}

           /// <summary>
           /// Desc:公司总机电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Tel {get;set;}

           /// <summary>
           /// Desc:传真
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Fax {get;set;}

           /// <summary>
           /// Desc:公司地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Address {get;set;}

           /// <summary>
           /// Desc:删除状态
           /// Default:b'0'
           /// Nullable:False
           /// </summary>           
           public bool DelStatus {get;set;}

           /// <summary>
           /// Desc:添加时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime AddTime {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime EditTIme {get;set;}

    }
}
