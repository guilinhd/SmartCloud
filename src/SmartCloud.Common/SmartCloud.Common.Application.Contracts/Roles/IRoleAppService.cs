

using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Roles
{
    public interface IRoleAppService : ICrudAppService<RoleDto, Guid>
    {
        Task<List<RoleDto>> GetListAsync();
    }
}
