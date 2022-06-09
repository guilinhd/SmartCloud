using System.Text.Json;
using JetBrains.Annotations;
using SmartCloud.Common.Datas;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Microsoft.Extensions.Options;

namespace SmartCloud.Common.DataIndexs
{
    public class DataIndexManager : DomainService
    {
        private readonly IDataIndexRepository _repository;
        private readonly DataManager _dataManager;
        private readonly IOptions<JsonSerializerOptions> _options;

        public DataIndexManager(
            IDataIndexRepository repository,
            DataManager dataManager,
            IOptions<JsonSerializerOptions> options
        )
        {
            _repository = repository;
            _dataManager = dataManager;
            _options = options;
        }

        /// <summary>
        /// 是否允许创建类别实体
        /// </summary>
        /// <param name="name">类别名称</param>
        /// <returns>类别实体</returns>
        /// <exception cref="DataIndexAlreadyExistsException">类别名称重复</exception>
        public async Task<DataIndex> CreateAsync([NotNull] string name
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingDataIndex = await _repository.GetLisAsync(QueryEnum.Single, name);
            if (existingDataIndex.Count > 0)
            {
                throw new DataIndexAlreadyExistsException(name);
            }

            #region 初始化类型描述
            List<Description> descriptions = new List<Description>();
            descriptions.Add(new Description()
            {
                No = 1,
                Name = "Name",
                Title = "名称",
                Content = ""
            });
            for (int i = 1; i <= 15; i++)
            {
                descriptions.Add(new Description()
                {
                    No = i + 1,
                    Width = 120,
                    Name = "Remark" + i.ToString(),
                    Title = "备注" + i.ToString(),
                    Content = ""
                });
            }
            #endregion

            return new DataIndex(
                GuidGenerator.Create(),
                name,
                JsonSerializer.Serialize(descriptions, _options.Value)
            ); 
        }

        /// <summary>
        /// 是否允许修改类别名称
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
        }

        /// <summary>
        /// 修改描述信息
        /// </summary>
        /// <param name="dataIndex">实体</param>
        /// <param name="descriptions">描述信息</param>
        public void UpdateDescription(
            [NotNull] DataIndex dataIndex,
            List<Description> descriptions
        )
        {
            Check.NotNull(dataIndex, nameof(dataIndex));
            dataIndex.Description = JsonSerializer.Serialize(descriptions, _options.Value);
        }

        /// <summary>
        /// 是否允许删除类别
        /// </summary>
        /// <param name="name">类别名称<</param>
        /// <exception cref="DataIndexHasDatasException">有数据字典信息,不能删除</exception>
        public async Task DeleteAsync([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            int count = await _dataManager.GetDatasCount(name);
            if (count > 0)
            {
                throw new DataIndexHasDatasException(name);
            }
        }
    }
}
