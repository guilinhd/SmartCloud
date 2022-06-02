using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.Attachments
{
    public class AttachmentManager : DomainService
    {
        private readonly IAttachmentRepository _repository;

        public AttachmentManager(IAttachmentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="query">查询类型</param>
        /// <param name="tableId">表id</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public async Task DeleteAsync(QueryEnum query, string tableId, string name)
        {
            var attachments = await _repository.GetListAsync(query, tableId, name);
            if (attachments.Count > 0)
            {
                await _repository.DeleteManyAsync(attachments);
            }
        }
    }
}
