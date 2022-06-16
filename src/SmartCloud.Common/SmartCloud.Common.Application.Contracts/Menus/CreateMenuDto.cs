using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;


namespace SmartCloud.Common.Menus
{
    public class CreateMenuDto 
    {
        public List<MenuDto> Menus { get; set; }

        public Dictionary<Guid, string> Roles { get; set; }

        public List<OrganizationDto> Organizations { get; set; }

        public List<PartUserDto> Users { get; set; }
    }
}
