

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
        Task<SaveRoleDto> CreateAsync(CreateSaveRoleDto dto);

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
        Task<SaveRoleDto> GetAsync(Guid id);

        /// <summary>
        /// 修改存盘
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<SaveRoleDto> UpdateAsync(UpdateSaveRoleDto dto);
    }
}
