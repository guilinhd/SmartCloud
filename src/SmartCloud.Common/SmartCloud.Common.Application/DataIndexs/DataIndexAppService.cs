using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Datas;
using Microsoft.Extensions.DependencyInjection;

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
        /// <returns></returns>
        [Route("api/common/dataindex/{name}")]
        public async Task<DataIndexDto> CreateAsync(string name)
        {
            //新增存盘
            var dataIndex = await _manager.CreateAsync(name);
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
            //删除
            await _manager.DeleteAsync(dataIndex);
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
        public async Task<DataIndexDto> UpdateAsync(
            Guid id, 
            string name)
        {
            var dataIndex = await _repository.GetAsync(id);

            //修改存盘
            await _manager.ChangeNameAsync(dataIndex, name);
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
            //修改存盘
            await _manager.UpdateAuthority(dataIndex, authority.Readers, authority.Editors);
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
            //修改存盘
            await _manager.UpdateDescription(dataIndex, descriptions);
        }
    }
}
