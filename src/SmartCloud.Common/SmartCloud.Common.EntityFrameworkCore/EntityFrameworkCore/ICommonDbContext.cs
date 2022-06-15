using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Datas;
using SmartCloud.Common.DataIndexs;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using SmartCloud.Common.Attachments;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;
using SmartCloud.Common.Menus;
using SmartCloud.Common.Roles;
using SmartCloud.Common.RoleUsers;
using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.Permissions;

namespace SmartCloud.Common.EntityFrameworkCore
{
    [ConnectionStringName("SmartCloud")]
    public interface ICommonDbContext : IEfCoreDbContext
    {
        DbSet<DataIndex> DataIndexs { get; }

        DbSet<Data> Datas { get; }

        DbSet<Attachment> Attachments { get; }

        DbSet<Organization> Organizations { get; }

        DbSet<User> Users { get; }

        DbSet<Permission> Permissions { get; }

        DbSet<Menu> Menus { get; }

        DbSet<Role> Roles { get; }

        DbSet<RoleUser> RoleUsers { get; }

        DbSet<RoleMenu> RoleMenus { get; }
    }
}
