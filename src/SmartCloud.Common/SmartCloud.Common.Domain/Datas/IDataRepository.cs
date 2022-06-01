using SmartCloud.Common.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Datas
{
    public interface IDataRepository : IRepository<Data, Guid>
    {
        Task<List<Data>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string category);

        Task<List<Data>> FindAllAsync(string category);

        Task<List<Data>> FindAllAsync(string category, string name);

        Task<List<Data>> FindAllAsync(string category, string name, string remark);
    }
}
