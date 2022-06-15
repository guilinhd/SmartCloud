using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Menus
{
    public class CreateMenuDto 
    {
        public List<MenuDto> Menus { get; set; }

        public Dictionary<Guid, string> Roles { get; set; }

        public List<OrganizationDto> Organizations { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
