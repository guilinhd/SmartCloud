using SmartCloud.Common.RoleUsers;
using SmartCloud.Common.RoleMenus;
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Roles
{
    public class RoleDto : AuditedEntityDto<Guid>
    {
        public  string Name { get; set; }

        public List<RoleUserDto> Users { get; set; }

        public List<RoleMenuDto> Menus { get; set; }
    }
}
