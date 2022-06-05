using AutoMapper;
using SmartCloud.Core.Organizations;
using System.Text.Json;

namespace SmartCloud.Core
{
    public class CoreApplicationAutoMapperProfile : Profile
    {
        public CoreApplicationAutoMapperProfile()
        {
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
