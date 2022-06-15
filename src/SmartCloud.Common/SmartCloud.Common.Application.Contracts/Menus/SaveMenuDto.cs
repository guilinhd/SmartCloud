using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.RoleUsers;

namespace SmartCloud.Common.Menus
{
    public class SaveMenuDto : MenuDto
    {
        public List<RoleMenuDto> RoleMenus { get; set; }

        public List<RoleUserDto> RoleUsers { get; set; }
    }
}
