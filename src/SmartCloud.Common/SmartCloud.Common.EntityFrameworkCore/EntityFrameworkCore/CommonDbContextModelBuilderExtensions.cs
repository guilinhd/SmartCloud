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
                b.Property(b => b.Reader).HasDefaultValue("");
                b.Property(b => b.Editor).HasDefaultValue("");
            });

            builder.Entity<Data>(b =>
            {
                b.ToTable("Datas");
                b.ConfigureByConvention();

                b.HasIndex(b => b.Category);
                b.HasIndex(b => new { b.Category, b.Name });
                b.HasIndex(b => new { b.Category, b.Name, b.Remark1 });

                b.Property(b => b.Remark1).HasDefaultValue("");
                b.Property(b => b.Remark2).HasDefaultValue("");
                b.Property(b => b.Remark3).HasDefaultValue("");
                b.Property(b => b.Remark4).HasDefaultValue("");
                b.Property(b => b.Remark5).HasDefaultValue("");
                b.Property(b => b.Remark6).HasDefaultValue("");
                b.Property(b => b.Remark7).HasDefaultValue("");
                b.Property(b => b.Remark8).HasDefaultValue("");
                b.Property(b => b.Remark9).HasDefaultValue("");
                b.Property(b => b.Remark10).HasDefaultValue("");
                b.Property(b => b.Remark11).HasDefaultValue("");
                b.Property(b => b.Remark12).HasDefaultValue("");
                b.Property(b => b.Remark13).HasDefaultValue("");
                b.Property(b => b.Remark14).HasDefaultValue("");
                b.Property(b => b.Remark15).HasDefaultValue("");
            });
        }
    }
}
