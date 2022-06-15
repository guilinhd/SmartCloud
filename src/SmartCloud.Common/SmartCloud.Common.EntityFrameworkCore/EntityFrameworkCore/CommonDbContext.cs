using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Attachments;
using SmartCloud.Common.DataIndexs;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Menus;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.Permissions;
using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.Roles;
using SmartCloud.Common.RoleUsers;
using SmartCloud.Common.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.EntityFrameworkCore
{
    [ConnectionStringName("SmartCloud")]
    public class CommonDbContext : AbpDbContext<CommonDbContext>, ICommonDbContext
    {
        public DbSet<DataIndex> DataIndexs { get; set; }

        public DbSet<Data> Datas { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RoleUser> RoleUsers { get; set; }

        public DbSet<RoleMenu> RoleMenus { get; set; }

        

        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCommon();
        }
    }
}
