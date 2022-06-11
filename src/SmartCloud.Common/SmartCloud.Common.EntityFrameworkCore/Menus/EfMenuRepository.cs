using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.Menus
{
    public class EfMenuRepository : EfCoreRepository<CommonDbContext, Menu, Guid>, IMenuRepository
    {
        public EfMenuRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Menu>> GetListAsync(QueryEnum query, string name)
        {
            var dbSet = await GetDbSetAsync();

            return query switch
            {
                QueryEnum.ParentId => await dbSet.Where(d => d.ParentId == name).ToListAsync(),
                _ => await dbSet.ToListAsync(),
            };
        }
    }
}
