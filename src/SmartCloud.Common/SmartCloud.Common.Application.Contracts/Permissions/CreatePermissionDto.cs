using SmartCloud.Common.Menus;
using SmartCloud.Common.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Permissions
{
    public class CreatePermissionDto
    {
        public string UserName { get; set; }

        public List<PermissionDto> Permissions { get; set; }

        public INodeDto Organization { get; set; }

        public INodeDto Menu { get; set; }
    }
}
