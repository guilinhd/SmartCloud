
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Roles
{
    public class PartRoleDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
