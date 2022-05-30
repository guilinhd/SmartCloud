using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.Datas
{
    public class DataManager : DomainService
    {
        private readonly IDataRepository _repository;

        public DataManager(IDataRepository dataRepository)
        {
            _repository = dataRepository;
        }

        public async Task DeleteAll(string category)
        {
            var datas = await _repository.FindAllAsync(category);

            await _repository.DeleteManyAsync(datas);
        }

        public async Task UpdateAll(string category, string newCatgory)
        {
            var datas = await _repository.FindAllAsync(category);

            Parallel.ForEach(datas, data => {
                data.Category = newCatgory;
            });

            await _repository.UpdateManyAsync(datas);
        }
    }
}
