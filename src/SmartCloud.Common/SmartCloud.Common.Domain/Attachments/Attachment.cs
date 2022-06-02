
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace SmartCloud.Common.Attachments
{
    public class Attachment : AuditedAggregateRoot<Guid>
    {
        [Required]
        public string TableId { get; set; }

        [Required]
        public string TableName { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string LastWriteTimeUtc { get; set; }

        public string Length { get; set; }

        public string Extension { get; set; }

        public string ServerPathName { get; set; }

        public string ServerFileName { get; set; }

        public int Status { get; set; }
    }
}
