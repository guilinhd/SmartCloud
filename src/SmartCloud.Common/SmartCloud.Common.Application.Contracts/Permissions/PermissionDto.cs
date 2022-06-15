using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Permissions
{
    public class PermissionDto : AuditedEntityDto<Guid>
    {
        public string UserName { get; set; }

        public string MenuId { get; set; }

        public string OrganizationAccounting { get; set; }

        /// <summary>
        /// 1-读  2-编辑
        /// </summary>
        public int Status { get; set; }
    }
}
