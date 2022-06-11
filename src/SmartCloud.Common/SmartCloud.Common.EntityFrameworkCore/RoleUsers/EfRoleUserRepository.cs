using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace SmartCloud.Common.RoleUsers
{
    public class EfRoleUserRepository : EfCoreRepository<CommonDbContext, RoleUser, Guid>, IRoleUserRepository
    {
        public EfRoleUserRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<RoleUser>> GetListAsync(QueryEnum query, string name)
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
