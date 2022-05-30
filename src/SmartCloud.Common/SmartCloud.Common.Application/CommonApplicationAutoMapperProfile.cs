using AutoMapper;
using SmartCloud.Common.DataIndexs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common
{
    public class CommonApplicationAutoMapperProfile : Profile
    {
        public CommonApplicationAutoMapperProfile()
        {
            CreateMap<DataIndex, DataIndexDto>();
        }
    }
}
