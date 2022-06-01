using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.DataIndexs
{
    public interface IDataIndexRepository : IRepository<DataIndex, Guid>
    {
        Task<List<DataIndex>> GetLisAsync(QueryEnum query, string name = "" );
    }
}
