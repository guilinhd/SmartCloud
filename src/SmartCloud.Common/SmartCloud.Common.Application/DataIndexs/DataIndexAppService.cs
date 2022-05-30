using SmartCloud.Common.DataIndexs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace SmartCloud.Common.DataIndexs
{
    public class DataIndexAppService : ApplicationService, IDataIndexAppService
    {
        private readonly IDataIndexRepository _repository;
        private readonly DataIndexManager _manager;

        public DataIndexAppService(
            IDataIndexRepository repository,
            DataIndexManager manager
            )
        {
            _repository = repository;
            _manager = manager;
        }

        public async Task<DataIndexDto> CreateAsync(string name)
        {
            DataIndex dataIndex = await _manager.Create(name);
            dataIndex = await _repository.InsertAsync(dataIndex);

            return ObjectMapper.Map<DataIndex, DataIndexDto>(dataIndex);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _manager.DeleteAsync(id);
        }

        public async Task<DataIndexDto> GetAsync(Guid id)
        {
            var dataIndex = await _repository.GetAsync(id);
            if (dataIndex != null)
            {
                return ObjectMapper.Map<DataIndex, DataIndexDto>(dataIndex);
            }

            return null;
        }

        public async Task<List<DataIndexDto>> GetListAsync(string name)
        {
            var datas = await _repository.FindAllByNameAsync(name);
            return ObjectMapper.Map<List<DataIndex>, List<DataIndexDto>>(datas);
        }

        public async Task<DataIndexDto> UpdateAsync(Guid id, string name)
        {
            var dataIndex = await _repository.GetAsync(id);

            if (dataIndex != null)
            {
                await _manager.ChangeNameAsync(dataIndex, name);
                await _repository.UpdateAsync(dataIndex);
                return ObjectMapper.Map<DataIndex, DataIndexDto>(dataIndex);
            }

            return null;
        }

        public async Task UpdateAuthorization(Guid id, string reader, string editor)
        {
            var dataIndex = await _repository.GetAsync(id);
            if (dataIndex != null)
            {
                dataIndex.Reader = reader;
                dataIndex.Editor = editor;

                await _repository.UpdateAsync(dataIndex);
            }
        }

        public async Task UpdateDescriptionsAsync(Guid id, List<Description> descriptions)
        {
            await _manager.UpdateDescriptionsAsync(id, descriptions);
        }
    }
}
