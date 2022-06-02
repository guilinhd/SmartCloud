using Volo.Abp.Domain.Entities;

namespace SmartCloud.Core.Organizations
{
    public class Organization : AggregateRoot<Guid>
    {
        public int No { get; set; }

        public string ParentId { get; set; } = null!;

        public int Category { get; set; }

        public string Name { get; set; } = null!;

        public string Type { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Fax { get; set; } = "";

        public string Accounting { get; set; } = null!;

        public string Description { get; set; } = "";
    }
}
