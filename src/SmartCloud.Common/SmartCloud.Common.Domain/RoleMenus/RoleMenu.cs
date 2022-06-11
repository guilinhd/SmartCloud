

using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.RoleMenus
{
    public class RoleMenu : AggregateRoot<Guid>
    {
        public string RoleId { get; set; }

        public string MenuId { get; set; }  
    }
}
