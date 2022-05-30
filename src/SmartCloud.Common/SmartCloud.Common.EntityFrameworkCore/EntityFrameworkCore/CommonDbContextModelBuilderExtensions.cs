using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Domain.DataIndexs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace SmartCloud.Common.EntityFrameworkCore
{
    public static class CommonDbContextModelBuilderExtensions
    {
        public static void ConfigureCommon([NotNull] this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<DataIndex>(b =>
            {
                b.ToTable("DataIndexs");
                b.ConfigureByConvention();

                b.HasIndex(b => b.Name);
            });
        }
    }
}
