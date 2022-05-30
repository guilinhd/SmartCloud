using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartCloud.Common.DataIndexs;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.Domain.DataIndexs
{
    public class DataIndexManager : DomainService
    {
        private IDataIndexRepository _dataIndexRepository;

        public DataIndexManager(IDataIndexRepository dataIndexRepository)
        {
            _dataIndexRepository = dataIndexRepository;
        }

        public async Task<DataIndex> Create(
            [NotNull] string name,
            List<Description> descriptions,
            string reader,
            string editor
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingAuthor = await _dataIndexRepository.FindByNameAsync(name);
            if (existingAuthor != null)
            {
                throw new DataIndexAlreadyExistsException(name);
            }

            return new DataIndex(
                GuidGenerator.Create(),
                name,
                descriptions,
                reader,
                editor
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] DataIndex dataIndex,
            [NotNull] string newName)
        {
            Check.NotNull(dataIndex, nameof(dataIndex));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingDataIndex = await _dataIndexRepository.FindByNameAsync(newName);
            if (existingDataIndex != null && existingDataIndex.Id != dataIndex.Id)
            {
                throw new DataIndexAlreadyExistsException(newName);
            }

            dataIndex.ChangeName(newName);
        }

        
    }
}
