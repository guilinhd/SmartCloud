using SmartCloud.Common.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Application.Contracts.Datas
{
    public interface IDataAppService : ICrudAppService<DataDto, Guid, GetDataListDto, DataDto>
    {

    }
}
