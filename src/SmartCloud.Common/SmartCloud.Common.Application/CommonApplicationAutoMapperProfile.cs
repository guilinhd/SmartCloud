using AutoMapper;
using SmartCloud.Common.DataIndexs;
using System.Text.Json;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Attachments;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;
using SmartCloud.Common.Menus;
using SmartCloud.Common.Roles;
using SmartCloud.Common.RoleUsers;
using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.Permissions;

namespace SmartCloud.Common
{
    public class CommonApplicationAutoMapperProfile : Profile
    {
        public CommonApplicationAutoMapperProfile()
        {
            CreateMap<DataIndex, DataIndexDto>()
                .ForMember(
                des => des.Descriptions,
                opt => opt.MapFrom(src => src.Description.ToDataIndexDescriptions()));

            CreateMap<Data, DataDto>().ReverseMap();

            CreateMap<Attachment, AttachmentDto>().ReverseMap();

            CreateMap<Organization, OrganizationDto>()
                .ForMember(
                des => des.Descriptions,
                opt => opt.MapFrom(src => src.Description.ToDescriptions()));

            CreateMap<User, CreateUpdateUserDto>()
                .ForMember(
                    des => des.Descriptions,
                    opt => opt.MapFrom(src => src.Description.ToDescriptions()));

            CreateMap<User, PartUserDto>();

            CreateMap<User, SaveUserDto>();

            CreateMap<User, FullUserDto>()
                .ForMember(
                    des => des.Descriptions,
                    opt => opt.MapFrom(src => src.Description.ToDescriptions()))
                .ForMember(
                    des => des.OrganizationName,
                    opt => opt.MapFrom(src => src.Organization.Name)
                )
                .ForMember(
                    des => des.OrganizationAccounting,
                    opt => opt.MapFrom(src => src.Organization.Accounting)
                );

            CreateMap<Permission, PermissionDto>().ReverseMap();

            CreateMap<Menu, MenuDto>();

            CreateMap<Menu, SaveMenuDto>();

            CreateMap<Menu, CreateSaveMenuDto>().ReverseMap();

            CreateMap<Role, SaveRoleDto>();

            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<RoleUser, RoleUserDto>();

            CreateMap<RoleMenu, RoleMenuDto>();
        }
    }

    public static class ProfileExtensions
    {
        public static List<DataIndexs.Description> ToDataIndexDescriptions(this string description)
        {
            return JsonSerializer.Deserialize<List<DataIndexs.Description>>(description);
        }

        public static List<Description> ToDescriptions(this string description)
        {
            return JsonSerializer.Deserialize<List<Description>>(description);
        }
    }
}
