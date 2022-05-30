using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Domain.DataIndexs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.EntityFrameworkCore
{
    [ConnectionStringName("SmartCloud")]
    public class CommonDbContext : AbpDbContext<CommonDbContext>, ICommonDbContext
    {
        public DbSet<DataIndex> DataIndexs { get; set; }

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
