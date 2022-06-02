using Volo.Abp.Modularity;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(CoreDomainSharedModule)
    )]
    public class CoreDomainModule : AbpModule
    {

    }
}