
namespace SmartCloud.Common.Users
{
    public class UpdateSaveUserDto : UserDto
    {
        /// <summary>
        /// 新增加的角色  string-角色id  guid-roleuserid
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        /// 删除的角色用户
        /// </summary>
        public string[] RoleUsers { get; set; }
    }
}
