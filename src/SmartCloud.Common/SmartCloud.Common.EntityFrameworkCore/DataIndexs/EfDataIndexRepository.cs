using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.DataIndexs;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.DataIndexs
{
    public class EfDataIndexRepository : EfCoreRepository<CommonDbContext, DataIndex, Guid>, IDataIndexRepository
    {
        public EfDataIndexRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<DataIndex>> GetLisAsync(QueryEnum query, string name = "")
        {
            var dbSet = await GetDbSetAsync();
            switch (query)
            {
                case QueryEnum.Reader:
                    return await dbSet.Where(d => d.Reader.Contains(name + ";") || d.Editor.Contains(name + ";")).ToListAsync();
                case QueryEnum.Single:
                    {
                        var dataIndexs = new List<DataIndex>();
                        var dataInex = await dbSet.FirstOrDefaultAsync(d => d.Name == name);
                        if (dataInex != null)
                        {
                            dataIndexs.Add(dataInex);
                        }
                        return dataIndexs;
                    }
                default: 
                    return await dbSet.ToListAsync();
            }
        }
    }
}
