using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Menus
{
    public class UpdateSaveMenuDto : MenuDto
    {
        public string[] Roles { get; set; }

        public string[] RoleMenus { get; set; }
    }
}
