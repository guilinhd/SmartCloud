using Microsoft.EntityFrameworkCore;
using SmartCloud.Core.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Core.EntityFrameworkCore
{
    [ConnectionStringName("SmartCloud")]
    public class CoreDbContext : AbpDbContext<CoreDbContext>, ICoreDbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {

        }

        public DbSet<Organization> Organizations {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
        }
    }
}
