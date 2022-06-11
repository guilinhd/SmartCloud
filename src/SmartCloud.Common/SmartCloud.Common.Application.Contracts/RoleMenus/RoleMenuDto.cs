

using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.RoleMenus
{
    public class RoleMenuDto : AuditedEntityDto<Guid>
    {
        public string RoleId { get; set; }

        public string MenuId { get; set; }
    }
}
