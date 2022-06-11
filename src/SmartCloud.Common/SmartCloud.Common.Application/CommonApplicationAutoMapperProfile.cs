﻿using AutoMapper;
using SmartCloud.Common.DataIndexs;
using System.Text.Json;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Attachments;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;

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

            CreateMap<User, ListUserDto>()
                .ForMember(
                    des => des.Descriptions,
                    opt => opt.MapFrom(src => src.Description.ToDescriptions()));

            CreateMap<User, PartUserDto>();
                

            CreateMap<User, UserDto>()
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
