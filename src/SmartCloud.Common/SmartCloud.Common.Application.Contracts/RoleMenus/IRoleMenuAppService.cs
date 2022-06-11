using Volo.Abp.Application.Services;

namespace SmartCloud.Common.RoleMenus
{
    public interface IRoleMenuAppService : IApplicationService
    {
        Task<List<RoleMenuDto>> CreateAsync(CreateRoleMenuDto dto);

        Task<List<RoleMenuDto>> GetListAsync();
    }
}
