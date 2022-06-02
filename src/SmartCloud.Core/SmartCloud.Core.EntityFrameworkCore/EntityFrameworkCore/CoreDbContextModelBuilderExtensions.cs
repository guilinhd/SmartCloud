using Microsoft.EntityFrameworkCore;
using SmartCloud.Core.Organizations;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace SmartCloud.Core.EntityFrameworkCore
{
    public static class CoreDbContextModelBuilderExtensions
    {
        public static void ConfigureCore([NotNull] this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

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
