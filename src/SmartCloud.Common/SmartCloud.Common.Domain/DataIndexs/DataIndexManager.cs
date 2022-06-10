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
            List<Description> descriptions = new();
            descriptions.Add(new Description()
            {
                No = 1,
                Width = 120,
                Name = "Name",
                Title = "名称",
                Content = ""
            });
            for (int i = 1; i <= 15; i++)
            {
                descriptions.Add(new Description()
                {
                    No = i + 1,
                    Width = i <= 5 ? 120 : 0,
                    Name = "Remark" + i.ToString(),
                    Title = "备注" + i.ToString(),
                    Content = ""
                });
            }
            #endregion

            var dataIndex = new DataIndex(
                GuidGenerator.Create(),
                name,
                JsonSerializer.Serialize(descriptions, _options.Value)
            );

            await _repository.InsertAsync(dataIndex);

            return dataIndex;
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

            //修改存盘
            dataIndex.ChangeName(newName);
            await _repository.UpdateAsync(dataIndex);

            //修改存盘数据字典对应的类别名称
            await _dataManager.UpdateAsync(oldName, newName);
        }

        /// <summary>
        /// 是否允许删除类别
        /// </summary>
        /// <param name="name">类别名称<</param>
        /// <exception cref="DataIndexHasDatasException">有数据字典信息,不能删除</exception>
        public async Task DeleteAsync(
            [NotNull] DataIndex dataIndex
        )
        {
            Check.NotNull(dataIndex, nameof(dataIndex));

            int count = await _dataManager.GetDatasCount(dataIndex.Name);
            if (count > 0)
            {
                throw new DataIndexHasDatasException(dataIndex.Name);
            }

            await _repository.DeleteAsync(dataIndex);
        }

        /// <summary>
        /// 查询类别名称列表
        /// </summary>
        /// <returns>类别名称列表</returns>

        public async Task<Dictionary<Guid, string>> GetListAsync()
        {
            Dictionary<Guid, string> dto = new();

            var dataIndexs = await _repository.GetListAsync();
            dataIndexs.ForEach(dataIndex =>
            {
                dto.Add(dataIndex.Id, dataIndex.Name);
            });

            return dto;
        }

        /// <summary>
        /// 查询类别名称列表-按当前用户名
        /// </summary>
        /// <param name="query">查询枚举</param>
        /// <param name="userName">用户名</param>
        /// <returns>类别名称列表</returns>

        public async Task<Dictionary<Guid, string>> GetListAsync(string userName)
        {
            Dictionary<Guid, string> dto = new();

            var dataIndexs = await _repository.GetLisAsync(QueryEnum.Reader, userName);
            dataIndexs.ForEach(dataIndex =>
            {
                dto.Add(dataIndex.Id, dataIndex.Name);
            });

            return dto;
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="dataIndex">实体</param>
        /// <param name="readers">读者</param>
        /// <param name="editors">编辑者</param>
        /// <returns></returns>
        public async Task UpdateAuthority(
            [NotNull] DataIndex dataIndex,
            ICollection<string> readers,
            ICollection<string> editors
        )
        {
            Check.NotNull(dataIndex, nameof(dataIndex));

            dataIndex.Reader = readers.Count == 0 ? "" : readers.JoinAsString(";") + ";";
            dataIndex.Editor = editors.Count == 0 ? "" : editors.JoinAsString(";") + ";";

            await _repository.UpdateAsync(dataIndex);
        }

        /// <summary>
        /// 修改描述信息
        /// </summary>
        /// <param name="dataIndex">实体</param>
        /// <param name="descriptions">描述信息</param>
        public async Task UpdateDescription(
            [NotNull] DataIndex dataIndex,
            List<Description> descriptions
        )
        {
            Check.NotNull(dataIndex, nameof(dataIndex));
            dataIndex.Description = JsonSerializer.Serialize(descriptions, _options.Value);

            await _repository.UpdateAsync(dataIndex);
        }
    }
}
