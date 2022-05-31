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

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>数据字典类别信息</returns>
        public async Task<DataIndexDto> CreateAsync(string name)
        {
            DataIndex dataIndex = await _manager.Create(name);
            dataIndex = await _repository.InsertAsync(dataIndex);

            string result = dataIndex.Description;
            return ObjectMapper.Map<DataIndex, DataIndexDto>(dataIndex);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">类别Id</param>
        /// <returns>类别信息</returns>
        public async Task DeleteAsync(Guid id)
        {
            await _manager.DeleteAsync(id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>类别信息</returns>
        public async Task<DataIndexDto> GetAsync(Guid id)
        {
            var dataIndex = await _repository.GetAsync(id);
            if (dataIndex != null)
            {
                return ObjectMapper.Map<DataIndex, DataIndexDto>(dataIndex);
            }

            return null;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">读者姓名</param>
        /// <returns>类别信息列表</returns>
        public async Task<List<DataIndexDto>> GetListAsync(string? name)
        {
            var datas = await _repository.FindAllByNameAsync(name);
            return ObjectMapper.Map<List<DataIndex>, List<DataIndexDto>>(datas);
        }

        /// <summary>
        /// 修改名称
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">名称</param>
        /// <returns>数据字典类别信息</returns>
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

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="reader">读者</param>
        /// <param name="editor">编辑者</param>
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

        /// <summary>
        /// 修改描述
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="descriptions">描述信息</param>
        /// <returns></returns>
        public async Task UpdateDescriptionsAsync(Guid id, List<Description> descriptions)
        {
            await _manager.UpdateDescriptionsAsync(id, descriptions);
        }
    }
}
