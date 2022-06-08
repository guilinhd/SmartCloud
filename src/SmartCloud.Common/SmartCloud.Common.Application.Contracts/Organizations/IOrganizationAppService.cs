

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
        Task<GetDataNameListDto> CreateAsync();

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, OrganizationDto dto);

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
        /// 按上级id查询
        /// </summary>
        /// <param name="parentId">上级组织结构id</param>
        /// <returns>实体列表</returns>
        Task<List<OrganizationDto>> GetListAsync(string parentId);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns>实体列表</returns>
        Task<List<OrganizationDto>> GetListAsync();
    }
}
