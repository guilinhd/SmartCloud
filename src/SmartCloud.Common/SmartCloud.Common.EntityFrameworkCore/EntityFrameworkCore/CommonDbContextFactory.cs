using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.EntityFrameworkCore
{
    public class CommonDbContextFactory : IDesignTimeDbContextFactory<CommonDbContext>
    {
        public CommonDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<CommonDbContext>()
                .UseMySql(configuration.GetConnectionString("SmartCloud"), MySqlServerVersion.LatestSupportedServerVersion);

            return new CommonDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SmartCloud.Common.DbMigration/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
