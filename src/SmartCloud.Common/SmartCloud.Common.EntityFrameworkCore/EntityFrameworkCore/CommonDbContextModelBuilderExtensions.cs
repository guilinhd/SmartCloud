using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.DataIndexs;
using SmartCloud.Common.Datas;
using System.Diagnostics.CodeAnalysis;
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

            builder.Entity<Data>(b =>
            {
                b.ToTable("Datas");
                b.ConfigureByConvention();

                b.HasIndex(b => b.Category);
                b.HasIndex(b => new { b.Category, b.Name });
                b.HasIndex(b => new { b.Category, b.Name, b.Remark1 });
            });
        }
    }
}
