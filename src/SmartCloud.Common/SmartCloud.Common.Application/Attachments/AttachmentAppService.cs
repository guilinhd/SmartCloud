using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace SmartCloud.Common.Attachments
{
    public class AttachmentAppService : CrudAppService<Attachment, AttachmentDto, Guid>, IAttachmentAppService
    {
        private readonly IAttachmentRepository _repository;
        private readonly AttachmentManager _manager;

        public AttachmentAppService(
            IAttachmentRepository repository,
            AttachmentManager manager
            ) : base(repository)
        {
            _repository = repository;
            _manager = manager;
        }

        /// <summary>
        /// 删除当前行的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <returns>附件信息</returns>
        [Route("api/common/attchment/tableid/{tableid}")]
        public async Task DeleteAsync(string tableId)
        {
            await _manager.DeleteAsync(QueryEnum.Row, tableId, "");
        }

        /// <summary>
        /// 删除当前行、文件的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverPathName">文件名称</param>
        /// <returns></returns>
        [Route("api/common/attchment/tableid/{tableid}/serverfilename/{serverfilename}")]
        public async Task DeleteAsync(string tableId, string serverFileName)
        {
            await _manager.DeleteAsync(QueryEnum.File, tableId, serverFileName);
        }

        /// <summary>
        /// 删除当前行、文件夹的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverPathName">文件夹名称</param>
        /// <returns></returns>
        [Route("api/common/attchment/tableid/{tableid}/serverpathname/{serverpathname}")]
        public async Task DeleteListAsync(string tableId, string serverPathName)
        {
            await _manager.DeleteAsync(QueryEnum.Folder, tableId, serverPathName);
        }

        /// <summary>
        /// 查询当前行的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <returns>附件信息</returns>
        [Route("api/common/attchment/tableid/{tableid}")]
        public async Task<List<AttachmentDto>> GetAsync(string tableId)
        {
            var attachments = await _repository.GetListAsync(QueryEnum.Row, tableId, "");
            return ObjectMapper.Map<List<Attachment>, List<AttachmentDto>>(attachments);
        }

        /// <summary>
        /// 查询当前行、文件的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverFileName">文件名</param>
        /// <returns>附件信息</returns>
        [Route("api/common/attchment/tableid/{tableid}/serverfilename/{serverfilename}")]
        public async Task<List<AttachmentDto>> GetAsync(string tableId, string serverFileName)
        {
            var attachments = await _repository.GetListAsync(QueryEnum.File, tableId, serverFileName);
            return ObjectMapper.Map<List<Attachment>, List<AttachmentDto>>(attachments);
        }

        /// <summary>
        /// 查询当前行、文件夹名称的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverPathName">文件夹名称</param>
        /// <returns>附件信息</returns>
        [Route("api/common/attchment/tableid/{tableid}/serverpathname/{serverpathname}")]
        public async Task<List<AttachmentDto>> GetListAsync(string tableId, string serverPathName)
        {
            var attachments = await _repository.GetListAsync(QueryEnum.Folder, tableId, serverPathName);
            return ObjectMapper.Map<List<Attachment>, List<AttachmentDto>>(attachments);
        }

        /// <summary>
        /// 批量查询行的附件信息
        /// </summary>
        /// <param name="tableIds">行id</param>
        /// <returns>附件信息</returns>
        [Route("api/common/attchment/tableids/{tableids}")]
        public async Task<List<AttachmentDto>> GetListAsync(string tableIds)
        {
            var attachments = await _repository.GetListAsync(QueryEnum.Rows, tableIds, "");
            return ObjectMapper.Map<List<Attachment>, List<AttachmentDto>>(attachments);
        }
    }
}
