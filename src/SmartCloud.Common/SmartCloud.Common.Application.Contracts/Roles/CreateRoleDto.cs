using SmartCloud.Common.Menus;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;


namespace SmartCloud.Common.Roles
{
    public class CreateRoleDto
    {
        public List<RoleDto> Roles { get; set; }

        public List<OrganizationDto> Organizations { get; set; }

        public List<PartUserDto> Users { get; set; }

        public List<MenuDto> Menus { get; set; }

        public ICollection<string> Datas { get; set; }
    }
}
