using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Datas;
using SmartCloud.Common.DataIndexs;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.EntityFrameworkCore
{
    [ConnectionStringName("SmartCloud")]
    public interface ICommonDbContext : IEfCoreDbContext
    {
        DbSet<DataIndex> DataIndexs { get; }

        DbSet<Data> Datas { get; }
    }
}
