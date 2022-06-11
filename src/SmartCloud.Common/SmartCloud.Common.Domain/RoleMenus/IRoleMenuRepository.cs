

using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.RoleMenus
{
    public interface IRoleMenuRepository : IRepository<RoleMenu, Guid>
    {
        Task<List<RoleMenu>> GetListAsync(QueryEnum query, string name);
    }
}
