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
    }
}
