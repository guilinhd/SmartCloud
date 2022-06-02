using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Attachments
{
    public interface IAttachmentAppService : ICrudAppService<AttachmentDto, Guid>
    {
        /// <summary>
        /// 查询当前行的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <returns></returns>
        Task<List<AttachmentDto>> GetAsync(string tableId);

        /// <summary>
        /// 查询当前行、文件的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverFileName">文件名</param>
        /// <returns></returns>
        Task<List<AttachmentDto>> GetAsync(string tableId, string serverFileName);

        /// <summary>
        /// 批量查询行的附件信息
        /// </summary>
        /// <param name="tableIds">行id</param>
        /// <returns></returns>
        Task<List<AttachmentDto>> GetListAsync(string tableIds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task DeleteAsync(string tableId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="serverFileName"></param>
        /// <returns></returns>
        Task DeleteAsync(string tableId, string serverFileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="serverPathName"></param>
        /// <returns></returns>
        Task DeleteListAsync(string tableId, string serverPathName);
    }
}
