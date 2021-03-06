using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;
using SmartCloud.Common.EntityFrameworkCore;

namespace SmartCloud.Core.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(CommonEntityFrameworkCoreModule)
    )]
    public class CoreEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CoreDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also TravelCrmMigrationsDbContextFactory for EF Core tooling. */
                options.UseMySQL();
            });
        }
    }
}