using Volo.Abp.Modularity;

namespace SmartCloud.Core.HttpApi
{
    [DependsOn(
        typeof(CoreApplicationContractsModule)
    )]
    public class CoreHttpApiModule : AbpModule
    {

    }
}