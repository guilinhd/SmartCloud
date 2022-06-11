using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Menus
{
    public interface IMenuRepository : IRepository<Menu, Guid>
    {
        Task<List<Menu>> GetListAsync(QueryEnum query, string name);
    }
}
