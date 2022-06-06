using Volo.Abp.Modularity;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(CoreApplicationContractsModule)
    )]
    public class CoreHttpApiModule : AbpModule
    {

    }
}