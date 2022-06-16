

namespace SmartCloud.Common.Roles
{
    public class UpdateSaveRoleDto : RoleDto
    {
        /// <summary>
        /// 新增用户Ids
        /// </summary>
        public string[] UserIds { get; set; }

        /// <summary>
        /// 删除角色用户Ids
        /// </summary>
        public string[] RoleUserIds { get; set; }

        /// <summary>
        /// 新增菜单Ids
        /// </summary>
        public string[] MenuIds { get; set; }

        /// <summary>
        /// 删除菜单Ids
        /// </summary>
        public string[] RoleMenuIds { get; set; }
    }
}
