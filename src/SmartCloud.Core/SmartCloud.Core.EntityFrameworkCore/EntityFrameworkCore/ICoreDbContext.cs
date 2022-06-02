using Microsoft.EntityFrameworkCore;
using SmartCloud.Core.Organizations;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Core.EntityFrameworkCore
{
    [ConnectionStringName("SmartCloud")]
    public interface ICoreDbContext : IEfCoreDbContext
    {
        DbSet<Organization> Organizations { get; }
    }
}
