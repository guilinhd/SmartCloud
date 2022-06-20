using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.DataIndexs;
using Volo.Abp.Application.Dtos;
using Volo.Abp;

namespace SmartCloud.Common.Datas
{
    public class DataAppService : CrudAppService<Data, DataDto, Guid, GetDataListDto, DataDto>, IDataAppService
    {
        private readonly IDataRepository _repository;
        private readonly DataManager _manager;
        private readonly DataIndexManager _dataIndexManager;

        public DataAppService(
            IDataRepository repository,
            DataManager manager,
            DataIndexManager dataIndexManager
        ) 
            : base(repository)
        {
            _repository = repository;
            _manager = manager;
            _dataIndexManager = dataIndexManager;
        }

        /// <summary>
        /// 显示数据字典类别
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Dictionary<Guid, string>> CreateAsync()
        {
            return await _dataIndexManager.GetListAsync();
        }

        /// <summary>
        /// 按类别名称批量删除
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/common/data/create/username/{userName}")]
        public async Task<Dictionary<Guid, string>> CreateAsync(string userName)
        {
            return await _dataIndexManager.GetListAsync(userName);
        }

        /// <summary>
        /// 按类别名称批量删除
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <returns></returns>
        [Route("api/common/data/category/{category}")]
        public async Task DeleteAsync(string category)
{
            await _manager.DeleteAsync(category);
        }

        /// <summary>
        /// 按类别名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <returns>数据字典信息列表</returns>
        [Route("api/common/data/category/{category}")]
        public async Task<List<DataDto>> GetListAsync(string category)
        {
            var datas = await _repository.GetListAsync(category);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }

        [RemoteService(false)]
        public override Task<PagedResultDto<DataDto>> GetListAsync(GetDataListDto input)
        {
            return base.GetListAsync(input);
        }
    }
}
