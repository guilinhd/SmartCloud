using Volo.Abp.Modularity;
using SmartCloud.Common.Application.Contracts;
namespace SmartCloud.Common.HttpApi
{
    [DependsOn(
        typeof(CommonApplicationContractsModule)
    )]
    public class CommonHttpApiModule : AbpModule
    {

    }
}