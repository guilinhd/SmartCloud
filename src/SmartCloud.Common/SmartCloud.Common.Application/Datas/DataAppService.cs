using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Datas
{
    public class DataAppService : CrudAppService<Data, DataDto, Guid, GetDataListDto, DataDto>, IDataAppService
    {
        public DataAppService(IDataRepository dataRepository) : base(dataRepository)
        {

        }
    }
}
