
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.RoleUsers
{
    public interface IRoleUserAppService : IApplicationService
    {
        Task<List<RoleUserDto>> CreateAsync(CreateRoleUserDto dto);

        Task<List<RoleUserDto>> GetListAsync(string roleId);
    }
}
