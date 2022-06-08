using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.Organizations
{
    public class EfOrganizationRepository : EfCoreRepository<CommonDbContext, Organization, Guid>, IOrganizationRepository
    {
        public EfOrganizationRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Organization>> GetListAsync(QueryEnum query, string name)
        {
            var dbSet = await GetDbSetAsync();

            switch (query)
            {
                case QueryEnum.Parent:
                    return await dbSet.Where(d => d.ParentId == name).ToListAsync();
                case QueryEnum.Name:
                    {
                        var organizations = new List<Organization>();
                        var organization = await dbSet.Where(d => d.Name == name).FirstOrDefaultAsync();
                        if (organization != null)
                        {
                            organizations.Add(organization);
                        }
                        return organizations;
                    }
                case QueryEnum.Type:
                    return await dbSet.Where(d => d.Type == name).ToListAsync();
                case QueryEnum.Category:
                    return await dbSet.Where(d => d.Category == Convert.ToInt16(name)).ToListAsync();
                case QueryEnum.Accounting:
                    return await dbSet.Where(d => d.Accounting.StartsWith(name)).ToListAsync();
                default:
                    return await dbSet.ToListAsync();
            }
        }
    }
}
