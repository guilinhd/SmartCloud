using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Domain.DataIndexs
{
    public interface IDataIndexRepository : IRepository<DataIndex, Guid>
    {
        Task<DataIndex> FindByNameAsync(string name);

        Task<List<DataIndex>> FindAllByNameAsync(string name);

    }
}
