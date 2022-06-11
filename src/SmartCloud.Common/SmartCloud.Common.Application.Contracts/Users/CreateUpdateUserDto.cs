using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Users
{
    public class ListUserDto : AuditedEntityDto<Guid>
    {
        public string OrganizationId { get; set; } = null!;

        public int No { get; set; }

        public string Name { get; set; } = null!;

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }

        public string Post { get; set; }

        /// <summary>
        /// 停用状态 0-正常 1-停用
        /// </summary>
        public int Disable { get; set; }

        public List<Description> Descriptions { get; set; }
    }
}
