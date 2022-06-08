using AutoMapper;
using SmartCloud.Common.DataIndexs;
using System.Text.Json;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Attachments;
using SmartCloud.Common.Organizations;

namespace SmartCloud.Common
{
    public class CommonApplicationAutoMapperProfile : Profile
    {
        public CommonApplicationAutoMapperProfile()
        {
            CreateMap<DataIndex, DataIndexDto>()
                .ForMember(
                des => des.Descriptions,
                opt => opt.MapFrom(src => src.Description.ToDescriptions()));

            CreateMap<Data, DataDto>().ReverseMap();

            CreateMap<Attachment, AttachmentDto>().ReverseMap();

            CreateMap<Organization, OrganizationDto>()
                .ForMember(
                des => des.Descriptions,
                opt => opt.MapFrom(src => src.Description.ToDescriptions()));
        }
    }

    public static class ProfileExtensions
    {
        public static List<Description> ToDescriptions(this string description)
        {
            return JsonSerializer.Deserialize<List<Description>>(description);
        }
    }
}
