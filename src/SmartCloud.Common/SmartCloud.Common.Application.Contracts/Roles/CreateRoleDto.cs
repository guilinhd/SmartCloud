﻿using SmartCloud.Common.Organizations;
using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.Users;


namespace SmartCloud.Common.Roles
{
    public class CreateRoleDto
    {
        public List<PartRoleDto> Roles { get; set; }

        public List<OrganizationDto> Organizations { get; set; }

        public List<PartUserDto> Users { get; set; }

        public List<RoleMenuDto> Menus { get; set; }

        public ICollection<string> Datas { get; set; }
    }
}