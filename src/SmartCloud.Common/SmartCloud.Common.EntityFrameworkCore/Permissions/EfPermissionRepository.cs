using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.Permissions
{
    public class EfPermissionRepository : EfCoreRepository<CommonDbContext, Permission, Guid>, IPermissionRepository
    {
        public EfPermissionRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Permission>> GetListAsync(string userName)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet.Where(f => f.UserName == userName).ToListAsync();
        }

        public async Task<List<Permission>> GetListAsync(IEnumerable<Guid> ids)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(f => ids.Contains(f.Id)).ToListAsync();
        }
    }
}
