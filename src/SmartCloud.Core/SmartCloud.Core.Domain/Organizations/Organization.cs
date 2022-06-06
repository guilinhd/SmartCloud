using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Unicode;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SmartCloud.Core.Organizations
{
    public class Organization : AggregateRoot<Guid>
    {
        public int No { get; set; }

        public string ParentId { get; set; } = null!;

        public int Category { get; set; }

        public string Name { get; private set; } = null!;

        public string Type { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Fax { get; set; } = "";

        public string Accounting { get; set; } = null!;

        public string Description { get; set; } = "";

        private Organization() { }
        
        internal Organization(
            Guid id,
            string parentId,
            int category,
            int no,
            string name,
            string type,
            string phone,
            string fax,
            string accounting,
            string description
        )
        {
            No = no;
            ParentId = parentId;
            Category = category;
            Name = name;
            Type = type;
            Phone = phone;
            Fax = fax;
            Accounting = accounting;
            Description = description;
        }

        internal Organization ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name)
            );
        }
    }
}
