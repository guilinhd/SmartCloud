using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.Users
{
    public class EfUserRepository : EfCoreRepository<CommonDbContext, User, Guid>, IUserRepository
    {
        public EfUserRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<User>> GetListAsync(QueryEnum query, string name)
        {
            var dbSet = await GetDbSetAsync();
            return query switch
            {
                QueryEnum.Name => await dbSet.Where(d => d.Name == name).ToListAsync(),
                QueryEnum.ParentId => await dbSet.Where(d => d.OrganizationId == name).ToListAsync(),
                QueryEnum.Post => await dbSet.Where(d => d.Post.Contains(name)).ToListAsync(),
                _ => await dbSet.ToListAsync(),
            };
        }
    }
}
