using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.DataIndexs
{
    public interface IDataIndexAppService : ICrudAppService<DataIndexDto, Guid, DataIndexDto, CreateUpdateDataIndexDto, DataIndexDto>
    {
        
    }
}
