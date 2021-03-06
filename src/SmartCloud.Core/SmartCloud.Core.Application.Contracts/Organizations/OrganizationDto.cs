
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Core.Organizations
{
    public class OrganizationDto : AuditedEntityDto<Guid>
    {
        public int No { get; set; }

        public string ParentId { get; set; } = null!;

        public int Category { get; set; }

        public string Name { get; private set; } = null!;

        public string Type { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Fax { get; set; } = "";

        public string Accounting { get; set; } = null!;

        public List<Description> Descriptions { get; set; } = new List<Description>();
    }
}
