using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.DataIndexs
{
    public interface IDataIndexRepository : IRepository<DataIndex, Guid>
    {
        Task<DataIndex> FindByNameAsync(string name);

        Task<List<DataIndex>> FindAllByNameAsync(string name);

    }
}
