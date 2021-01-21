namespace FytSoa.Core.Model.Sys
{
    /// <summary>
    /// 权限-菜单对应的功能列表
    /// </summary>
    public class SysBtnFun
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; } = false;
    }
}
