using Volo.Abp.Modularity;

namespace SmartCloud.Common
{
    [DependsOn(
        typeof(CommonApplicationContractsModule)
    )]
    public class CommonHttpApiModule : AbpModule
    {

    }
}