
using SmartCloud.Common.Permissions;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dtos">实体</param>
        /// <returns></returns>
        Task<List<PermissionDto>> CreateAsync(List<PermissionDto> dtos);

        /// <summary>
        /// 新增初始化
        /// </summary>
        /// <returns></returns>
        Task<CreatePermissionDto> CreateAsync(string userName);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteAsync(string[] ids);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dtos">实体列表</param>
        /// <returns></returns>
        Task UpdateAsync(PermissionDto dto);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="dtos">实体列表</param>
        /// <returns></returns>
        Task UpdateAsync(List<PermissionDto> dtos);

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="dto">实体列表</param>
        /// <returns></returns>
        Task SaveAsync(SavePermissionDto dto);
    }
}
