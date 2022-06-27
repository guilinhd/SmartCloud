using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Permissions
{
    public interface IPermissionRepository : IRepository<Permission, Guid>
    {
        Task<List<Permission>> GetListAsync(string userName);

        Task<List<Permission>> GetListAsync(IEnumerable<Guid> ids);
    }
}
