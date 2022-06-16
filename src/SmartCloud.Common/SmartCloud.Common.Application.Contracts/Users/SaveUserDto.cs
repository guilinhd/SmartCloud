using SmartCloud.Common.RoleUsers;


namespace SmartCloud.Common.Users
{
    public class SaveUserDto : UserDto
    {
        public List<RoleUserDto> RoleUses { get; set; }

        /// <summary>
        /// 当前用户拥有的菜单id数组
        /// </summary>
        public string[] Menus { get; set; }
    }
}
