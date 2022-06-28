
using SmartCloud.Common.Permissions;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        /// <summary>
        /// 新增初始化
        /// </summary>
        /// <returns></returns>
        Task<CreatePermissionDto> CreateAsync(string userName);

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="dto">实体列表</param>
        /// <returns></returns>
        Task SaveAsync(SavePermissionDto dto);
    }
}
