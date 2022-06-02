using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace SmartCloud.Core.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
    public class CoreEntityFrameworkCoreModule : AbpModule
    {
        
    }
}