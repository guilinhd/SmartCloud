

using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Roles
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        Task<Role> GetAsync(string name);

        Task<List<Role>> GetListAsync();
    }
}
