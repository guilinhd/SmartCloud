using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Domain.DataIndexs;
using SmartCloud.Common.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.DataIndexs
{
    public class EfDataIndexRepository : EfCoreRepository<CommonDbContext, DataIndex, Guid>, IDataIndexRepository
    {
        public EfDataIndexRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<DataIndex>> FindAllByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            
            if (name == "")
            {
                return await dbSet.ToListAsync();
            }
            else
            {
                return await dbSet.Where(d => d.Reader.Contains(name + ";")).ToListAsync();
            }
        }

        public async Task<DataIndex> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(d => d.Name == name);
        }
    }
}
