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

        public DataManager(IDataRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteAllByCategoryName(string name)
        {
            var datas = await _repository.FindAllAsync(name);

            await _repository.DeleteManyAsync(datas);
        }

        public async Task ChangeAllByCategoryName(string oldName, string newName)
        {
            var datas = await _repository.FindAllAsync(oldName);

            Parallel.ForEach(datas, data => {
                data.Category = newName;
            });

            await _repository.UpdateManyAsync(datas);
        }

        
    }
}
