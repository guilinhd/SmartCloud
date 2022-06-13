
using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.Roles
{
    public class EfRoleRepository : EfCoreRepository<CommonDbContext, Role, Guid>, IRoleRepository
    {
        public EfRoleRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Role> GetAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(d => d.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Role>> GetListAsync()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.ToListAsync();
        }
    }
}
