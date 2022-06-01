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
        private readonly IDataRepository _dataRepository;

        public DataAppService(IDataRepository dataRepository) : base(dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<List<DataDto>> FindAllAsync(string category)
        {
            var datas = await _dataRepository.FindAllAsync(category);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }

        public async Task<List<DataDto>> FindAllAsync(string category, string name)
        {
            var datas = await _dataRepository.FindAllAsync(category, name);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }

        public async Task<List<DataDto>> FindAllAsync(string category, string name, string remark)
        {
            var datas = await _dataRepository.FindAllAsync(category, name, remark);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }
    }
}
