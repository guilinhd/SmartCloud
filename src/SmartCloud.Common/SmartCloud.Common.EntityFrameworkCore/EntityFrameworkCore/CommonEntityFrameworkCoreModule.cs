using SmartCloud.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace SmartCloud.Common.EntityFrameworkCore.EntityFrameworkCore
{
    [DependsOn(
        typeof(CommonDomainModule)
    )]
    public class CommonEntityFrameworkCoreModule : AbpModule
    {
    }
}
