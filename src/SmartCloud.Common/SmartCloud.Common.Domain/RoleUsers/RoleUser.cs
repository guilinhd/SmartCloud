
using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.RoleUsers
{
    public class RoleUser : AggregateRoot<Guid>
    {
        public string RoleId { get; set; }

        public string UserId { get; set; }
    }
}
