
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.RoleUsers
{
    public interface IRoleUserRepository : IRepository<RoleUser, Guid>
    {
        Task<List<RoleUser>> GetListAsync(QueryEnum query, string name);
    }
}
