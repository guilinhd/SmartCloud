using SmartCloud.Common.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Users
{
    public class CreateUserDto
    {
        public List<OrganizationDto> Organizations { get; set; } = new();

        public List<PartUserDto> Users { get; set; } = new();

        public ICollection<string> Datas { get; set; }
    }
}
