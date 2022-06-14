

using SmartCloud.Common.Datas;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Organizations
{
    public interface IOrganizationAppService : IApplicationService
    {
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        Task<OrganizationDto> CreateAsync(OrganizationDto dto);

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns>数据字典</returns>
        Task<CreateOrganizationDto> CreateAsync();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<OrganizationDto> GetAsync(Guid id);

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, OrganizationDto dto);
    }
}
