using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Attachments
{
    public class AttachmentDto : AuditedEntityDto<Guid>
    {
        [Required]
        public string TableId { get; set; }

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
