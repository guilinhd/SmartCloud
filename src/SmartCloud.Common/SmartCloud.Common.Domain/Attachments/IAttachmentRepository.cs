using SmartCloud.Common.DataIndexs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Attachments
{
    public interface IAttachmentRepository : IRepository<Attachment, Guid>
    {
        Task<List<Attachment>> GetListAsync(QueryEnum query, string tableId, string name);
    }
}
