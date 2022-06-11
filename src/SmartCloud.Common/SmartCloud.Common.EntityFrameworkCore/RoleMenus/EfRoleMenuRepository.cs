

using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.RoleMenus
{
    public class EfRoleMenuRepository : EfCoreRepository<CommonDbContext, RoleMenu, Guid>, IRoleMenuRepository
    {
        public EfRoleMenuRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<RoleMenu>> GetListAsync(QueryEnum query, string name)
        {
            var dbSet = await GetDbSetAsync();
            return query switch
            {
                QueryEnum.RoleId => await dbSet.Where(d => d.RoleId == name).ToListAsync(),
                _ => await dbSet.ToListAsync(),
            };
        }
    }
}
