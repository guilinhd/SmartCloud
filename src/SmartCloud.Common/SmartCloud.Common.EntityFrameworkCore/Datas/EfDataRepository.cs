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



        public async Task<List<Data>> FindAllAsync(string category)
        {
            var dbSet = await GetDbSetAsync();


            return await dbSet
                .Where(d => d.Category == category)
                .ToListAsync();
        }

        public async Task<List<Data>> FindAllByNameAndRemarkAsync(string category, string name, string remark)
        {
            var dbSet = await GetDbSetAsync();
            if (name == "")
            {
                return await dbSet
                .Where(d => d.Category == category && d.Remark1 == remark)
                .ToListAsync();
            }
            else
            {
                return await dbSet
                .Where(d => d.Category == category && d.Name == name && d.Remark1 == remark)
                .ToListAsync();
            }

        }

        public async Task<List<Data>> FindAllByNameAsync(string category, string name)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet
                .Where(d => d.Category == category && d.Name == name)
                .ToListAsync();
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
    }
}
