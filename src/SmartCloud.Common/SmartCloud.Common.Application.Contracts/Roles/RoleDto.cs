using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Roles
{
    public class RoleDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
