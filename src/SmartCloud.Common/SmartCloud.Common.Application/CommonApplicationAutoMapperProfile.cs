using AutoMapper;
using SmartCloud.Common.DataIndexs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Unicode;
using SmartCloud.Common.Datas;

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
