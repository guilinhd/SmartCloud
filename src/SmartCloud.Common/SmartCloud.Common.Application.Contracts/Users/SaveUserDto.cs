using SmartCloud.Common.RoleUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Users
{
    public class SaveUserDto : UserDto
    {
        public List<RoleUserDto> RoleUses { get; set; }
    }
}
