using SmartCloud.Common.RoleUsers;
using SmartCloud.Common.RoleMenus;

namespace SmartCloud.Common.Roles
{
    public class SaveRoleDto : RoleDto
    {
        public List<RoleUserDto> Users { get; set; }

        public List<RoleMenuDto> Menus { get; set; }
    }
}
