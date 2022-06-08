using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Attachments;
using SmartCloud.Common.DataIndexs;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Organizations;
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
                b.ToTable("DataIndex");
                b.ConfigureByConvention();

                b.HasIndex(b => b.Name);
            });

            builder.Entity<Data>(b =>
            {
                b.ToTable("Data");
                b.ConfigureByConvention();

                b.HasIndex(b => b.Category);
                b.HasIndex(b => new { b.Category, b.Name });
                b.HasIndex(b => new { b.Category, b.Remark1 });
                b.HasIndex(b => new { b.Category, b.Name, b.Remark1 });
            });

            builder.Entity<Attachment>(b => {
                b.ToTable("Attachment");
                b.ConfigureByConvention();

                b.HasIndex(b => b.TableId);
                b.HasIndex(b => new { b.TableId, b.ServerPathName });
                b.HasIndex(b => new { b.TableId, b.ServerFileName });
            });

            builder.Entity<Organization>(b => {
                b.ToTable("Organization");
                b.ConfigureByConvention();

                b.HasIndex(b => b.Name);
                b.HasIndex(b => b.ParentId);
                b.HasIndex(b => b.Accounting);
                b.HasIndex(b => b.Category);
                b.HasIndex(b => b.Type);
            });
        }
    }
}
