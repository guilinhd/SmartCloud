using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using SmartCloud.Common.Datas;

namespace SmartCloud.Common.DataIndexs
{
    public class DataIndexAppService : ApplicationService, IDataIndexAppService
    {
        private readonly IDataIndexRepository _repository;
        private readonly DataIndexManager _manager;
        private readonly IDataAppService _dataAppService;

        public DataIndexAppService(
            IDataIndexRepository repository,
            DataIndexManager manager,
            IDataAppService dataAppService
            )
        {
            _repository = repository;
            _manager = manager;
            _dataAppService = dataAppService;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [Route("api/common/dataindex/{name}")]
        public async Task<DataIndexDto> CreateAsync(string name)
        {
            //生成实例
            DataIndex dataIndex = await _manager.CreateAsync(name);

            //新增存盘
            await _repository.InsertAsync(dataIndex);
            return ObjectMapper.Map<DataIndex, DataIndexDto>(dataIndex);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [Route("api/common/dataindex/{id}")]
        public async Task DeleteAsync(Guid id)
        {
            var dataIndex = await _repository.GetAsync(id);
            //是否允许删除
            await _manager.DeleteAsync(dataIndex.Name);

            //删除
            await _repository.DeleteAsync(dataIndex);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [Route("api/common/dataindex/{id}")]
        public async Task<DataIndexDto> GetAsync(Guid id)
        {
            var dataIndex = await _repository.GetAsync(id);
            return ObjectMapper.Map<DataIndex, DataIndexDto>(dataIndex);
        }

        /// <summary>
        /// 按用户名查询       
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns>实体列表</returns>
        [Route("api/common/dataindex/name/{name}")]
        public async Task<List<DataIndexDto>> GetListAsync(string name)
        {
            var datas = await _repository.GetLisAsync(QueryEnum.Reader, name);
            return ObjectMapper.Map<List<DataIndex>, List<DataIndexDto>>(datas);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns>实体列表</returns>
        [Route("api/common/dataindex/")]
        public async Task<List<DataIndexDto>> GetListAsync()
        {
            var datas = await _repository.GetLisAsync(QueryEnum.All);
            return ObjectMapper.Map<List<DataIndex>, List<DataIndexDto>>(datas);
        }

        /// <summary>
        /// 修改名称
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">名称</param>
        /// <returns>数据字典类别信息</returns>
        [Route("api/common/dataindex/id/{id}/name/{name}")]
        public async Task<DataIndexDto> UpdateAsync(Guid id, string name)
        {
            var dataIndex = await _repository.GetAsync(id);

            //修改前的类别名称
            string oldCategory = dataIndex.Name;

            //是否允许更改名称
            await _manager.ChangeNameAsync(dataIndex, name);

            //修改存盘
            await _repository.UpdateAsync(dataIndex);

            //修改存盘数据字典对应的类别名称
            await _dataAppService.UpdateAsync(oldCategory, name);

            return ObjectMapper.Map<DataIndex, DataIndexDto>(dataIndex);
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="authority">权限</param>
        [Route("api/common/dataindex/update/{id}")]
        public async Task UpdateAsync(Guid id, AuthorityDto authority)
        {
            var dataIndex = await _repository.GetAsync(id);
            dataIndex.Reader = authority.Readers.Count == 0 ? "" : authority.Readers.JoinAsString(";") + ";";
            dataIndex.Editor = authority.Editors.Count == 0 ? "" : authority.Editors.JoinAsString(";") + ";";

            //修改存盘
            await _repository.UpdateAsync(dataIndex);
        }

        /// <summary>
        /// 修改描述
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="descriptions">描述信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/common/dataindex/update/{id}")]
        public async Task UpdateAsync(Guid id, List<Description> descriptions)
        {
            var dataIndex = await _repository.GetAsync(id);
            if (dataIndex != null)
            {
                //修改描述信息
                _manager.UpdateDescription(dataIndex, descriptions);

                //修改存盘
                await _repository.UpdateAsync(dataIndex);
            }
        }
    }
}
