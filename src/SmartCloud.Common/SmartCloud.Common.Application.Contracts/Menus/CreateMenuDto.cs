using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;


namespace SmartCloud.Common.Menus
{
    public class CreateMenuDto 
    {
        public INodeDto Menu { get; set; }

        public Dictionary<Guid, string> Roles { get; set; }

        public INodeDto Organization { get; set; }

        public List<PartUserDto> Users { get; set; }
    }
}
