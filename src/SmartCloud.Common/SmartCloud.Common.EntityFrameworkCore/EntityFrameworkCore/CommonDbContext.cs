using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Attachments;
using SmartCloud.Common.DataIndexs;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.EntityFrameworkCore
{
    [ConnectionStringName("SmartCloud")]
    public class CommonDbContext : AbpDbContext<CommonDbContext>, ICommonDbContext
    {
        public DbSet<DataIndex> DataIndexs { get; set; }

        public DbSet<Data> Datas { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<Organization> Organizations => throw new NotImplementedException();

        public DbSet<User> Users => throw new NotImplementedException();

        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCommon();
        }
    }
}
