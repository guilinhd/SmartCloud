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
        /// 删除当前行的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <returns></returns>
        Task DeleteAsync(string tableId);

        /// <summary>
        /// 删除当前行、文件的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverFileName">文件名</param>
        /// <returns></returns>
        Task DeleteAsync(string tableId, string serverFileName);

        /// <summary>
        /// 删除当前行、文件夹的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverPathName">文件夹名称</param>
        /// <returns></returns>
        Task DeleteListAsync(string tableId, string serverPathName);

        /// <summary>
        /// 查询当前行的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <returns>附件信息</returns>
        Task<List<AttachmentDto>> GetAsync(string tableId);

        /// <summary>
        /// 查询当前行、文件的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverFileName">文件名</param>
        /// <returns>附件信息</returns>
        Task<List<AttachmentDto>> GetAsync(string tableId, string serverFileName);

        /// <summary>
        /// 查询当前行、文件夹名称的附件信息
        /// </summary>
        /// <param name="tableId">行id</param>
        /// <param name="serverPathName">文件夹名称</param>
        /// <returns>附件信息</returns>
        Task<List<AttachmentDto>> GetListAsync(string tableId, string serverPathName);

        /// <summary>
        /// 批量查询行的附件信息
        /// </summary>
        /// <param name="tableIds">行id</param>
        /// <returns>附件信息</returns>
        Task<List<AttachmentDto>> GetListAsync(string tableIds);

        
    }
}
