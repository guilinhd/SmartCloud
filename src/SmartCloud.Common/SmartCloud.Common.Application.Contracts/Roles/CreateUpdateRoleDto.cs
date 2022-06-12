
using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.RoleUsers;
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Roles
{
    public class CreateUpdateRoleDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string[] UserIds { get; set; }

        public string[] RoleUserIds { get; set; } 

        public string[] MenuIds { get; set; }   

        public string[] RoleMenuIds { get; set; }
    }
}
