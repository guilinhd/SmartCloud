

using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.RoleUsers
{
    public class RoleUserDto : AuditedEntityDto<Guid>
    {
        public string RoleId { get; set; }

        public string UserId { get; set; }
    }
}
