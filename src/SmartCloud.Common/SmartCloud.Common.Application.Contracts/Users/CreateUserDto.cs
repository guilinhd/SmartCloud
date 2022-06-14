using SmartCloud.Common.Menus;
using SmartCloud.Common.Organizations;


namespace SmartCloud.Common.Users
{
    public class CreateUserDto
    {
        public List<OrganizationDto> Organizations { get; set; } = new();

        public List<PartUserDto> Users { get; set; } = new();

        public List<MenuDto> Menus { get; set; } 

        public Dictionary<Guid, string> Roles { get; set; }

        public ICollection<string> Datas { get; set; }
    }
}
