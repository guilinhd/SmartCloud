
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Users
{
    public class PartUserDto : AuditedEntityDto<Guid>
    {
        public string OrganizationId { get; set; } = null!;

        public int No { get; set; }

        public string Name { get; set; } = null!;

        public int Disable { get; set; }
    }
}