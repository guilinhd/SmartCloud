

using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<RoleDto> CreateAsync(CreateUpdateRoleDto dto);

        /// <summary>
        /// 新增初始化
        /// </summary>
        /// <returns></returns>
        Task<CreateRoleDto> CreateAsync();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RoleDto> GetAsync(Guid id);

        /// <summary>
        /// 修改存盘
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<RoleDto> UpdateAsync(CreateUpdateRoleDto dto);
    }
}
