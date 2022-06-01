using System.Linq.Dynamic.Core;
using SmartCloud.Common.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.Datas
{
    public class EfDataRepository
        : EfCoreRepository<CommonDbContext, Data, Guid>,
          IDataRepository
    {
        public EfDataRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Data>> GetListAsync(int skipCount, int maxResultCount, string sorting, string category)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !category.IsNullOrWhiteSpace(),
                    d => d.Category == category
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<List<Data>> GetListAsync(string category)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(d => d.Category == category)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<List<Data>> GetListAsync(string category, string name)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet
                .Where(d => d.Category == category && d.Name == name)
                .ToListAsync();
        }

        public async Task<List<Data>> GetListAsync(string category, string name, string remark)
        {
            var dbSet = await GetDbSetAsync();
            if (name.IsNullOrEmpty())
            {
                return await dbSet
                .Where(d => d.Category == category && d.Remark1 == remark)
                .OrderBy(d=>d.Name)
                .ToListAsync();
            }
            else
            {
                return await dbSet
                .Where(d => d.Category == category && d.Name == name && d.Remark1 == remark)
                .ToListAsync();
            }
        }
    }
}
