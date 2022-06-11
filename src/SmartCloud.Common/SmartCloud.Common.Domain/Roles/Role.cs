using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.Roles
{
    public class Role : AggregateRoot<Guid>
    {
        public string Name { get; set; }    
    }
}
