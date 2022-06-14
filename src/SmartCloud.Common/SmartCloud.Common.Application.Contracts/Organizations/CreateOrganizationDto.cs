using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Organizations
{
    public class CreateOrganizationDto
    {
        public List<OrganizationDto> Organizations { get; set; }

        public ICollection<string> Datas { get; set; }
    }
}
