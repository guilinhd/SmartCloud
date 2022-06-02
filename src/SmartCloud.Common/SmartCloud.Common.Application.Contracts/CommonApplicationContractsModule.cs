using Volo.Abp.Modularity;

namespace SmartCloud.Common
{
    [DependsOn(
        typeof(CommonDomainSharedModule)
    )]
    public class CommonApplicationContractsModule : AbpModule
    {

    }
}