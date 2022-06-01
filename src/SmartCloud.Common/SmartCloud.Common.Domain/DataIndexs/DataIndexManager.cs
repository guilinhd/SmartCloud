using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartCloud.Common.Datas;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.DataIndexs
{
    public class DataIndexManager : DomainService
    {
        private readonly IDataIndexRepository _repository;
        private readonly IDataRepository _dataRepository;
        private readonly DataManager _dataManager;

        public DataIndexManager(
            IDataIndexRepository repository,
            IDataRepository dataRepository,
            DataManager dataManager
            )
        {
            _repository = repository;
            _dataRepository = dataRepository;
            _dataManager = dataManager;
        }

        /// <summary>
        /// 创建类别实体
        /// </summary>
        /// <param name="name">类别名称</param>
        /// <returns>类别实体</returns>
        /// <exception cref="DataIndexAlreadyExistsException">类别名称重复</exception>
        public async Task<DataIndex> Create([NotNull] string name
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingDataIndex = await _repository.GetLisAsync(QueryEnum.Single, name);
            if (existingDataIndex.Count == 0)
            {
                throw new DataIndexAlreadyExistsException(name);
            }

            return new DataIndex(GuidGenerator.Create(), name);
        }

        /// <summary>
        /// 修改类别名称
        /// </summary>
        /// <param name="dataIndex">类别实体</param>
        /// <param name="newName">类别新名称</param>
        /// <exception cref="DataIndexAlreadyExistsException">类别名称重复</exception>
        public async Task ChangeNameAsync(
            [NotNull] DataIndex dataIndex,
            [NotNull] string newName)
        {
            Check.NotNull(dataIndex, nameof(dataIndex));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            string oldName = dataIndex.Name;

            var existingDataIndex = await _repository.GetLisAsync(QueryEnum.Single, newName);
            if (existingDataIndex.Count > 0 && existingDataIndex.First().Id != dataIndex.Id)
            {
                throw new DataIndexAlreadyExistsException(newName);
            }

            dataIndex.ChangeName(newName);
            await _repository.UpdateAsync(dataIndex);
            await _dataManager.ChangeAllByCategoryName(oldName, newName);
        }

        /// <summary>
        /// 修改类别描述信息
        /// </summary>
        /// <param name="id">类别id</param>
        /// <param name="descriptions">描述信息</param>
        public async Task UpdateAsync(Guid id, List<Description> descriptions)
        {
            var dataIndex = await _repository.GetAsync(id);

            if (dataIndex != null)
            {
                dataIndex.Description = JsonSerializer.Serialize(
                    descriptions,
                    new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All) }
                );
                await _repository.UpdateAsync(dataIndex);
            }
        }

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="id">类别id<</param>
        /// <exception cref="DataIndexHasDatasException">有数据字典信息,不能删除</exception>
        public async Task DeleteAsync(Guid id)
        {
            var dataIndex = await _repository.GetAsync(id);
            if (dataIndex != null)
            {
                var datas = await _dataRepository.GetListAsync(dataIndex.Name);
                if (datas.Count > 0)
                {
                    throw new DataIndexHasDatasException(dataIndex.Name);
                }

                await _repository.DeleteAsync(dataIndex);
            }
        }
    }
}
