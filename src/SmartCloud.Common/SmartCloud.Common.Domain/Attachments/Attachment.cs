
using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.Attachments
{
    public class Attachment : AggregateRoot<Guid>
    {
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
