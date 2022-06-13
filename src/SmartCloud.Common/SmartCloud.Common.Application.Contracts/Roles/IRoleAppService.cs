

using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task<RoleDto> CreateAsync(CreateUpdateRoleDto dto);

        Task<CreateRoleDto> CreateAsync();

        Task DeleteAsync(Guid id);

        Task<RoleDto> GetAsync(Guid id);

        Task<RoleDto> UpdateAsync(CreateUpdateRoleDto dto);
    }
}
