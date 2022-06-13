using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.Roles
{
    public class Role : AggregateRoot<Guid>
    {
        public string Name { get; private set; }   
        
        private Role() { }

        internal Role(
            Guid id,
            string name
        ) : base(id)
        {
            Name = name;
        }

        internal Role ChangeName([NotNull] string name)
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
