using SmartCloud.Common.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Datas
{
    public interface IDataAppService : ICrudAppService<DataDto, Guid, GetDataListDto, DataDto>
    {
        Task<List<DataDto>> FindAllAsync(string category);

        Task<List<DataDto>> FindAllAsync(string category, string name);

        Task<List<DataDto>> FindAllAsync(string category, string name, string remark);
    }
}
