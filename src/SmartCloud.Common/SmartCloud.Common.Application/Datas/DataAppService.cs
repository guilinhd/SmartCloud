using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace SmartCloud.Common.Datas
{
    public class DataAppService : CrudAppService<Data, DataDto, Guid, GetDataListDto, DataDto>, IDataAppService
    {
        private readonly IDataRepository _repository;
        private readonly DataManager _manager;

        public DataAppService(
            IDataRepository repository,
            DataManager manager) 
            : base(repository)
        {
            _repository = repository;
            _manager = manager;
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

        /// <summary>
        /// 按类别名称、数据字典名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <returns>数据字典信息列表</returns>
        [RemoteService(false)]
        public async Task<List<DataDto>> GetListAsync(string category, string name)
        {
            var datas = await _repository.GetListAsync(category, name);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }

        /// <summary>
        /// 按类别名称、数据字典名称、数据字典备注查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <param name="remark">数据字典备注</param>
        /// <returns>数据字典信息列表</returns>
        [RemoteService(false)]
        public async Task<List<DataDto>> GetListAsync(string category, string name, string remark)
        {
            var datas = await _repository.GetListAsync(category, name, remark);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }

        /// <summary>
        /// 按类别名称批量查询
        /// </summary>
        /// <param name="categoires">类别名称数组</param>
        /// <returns>数据字典信息列表</returns>

        [RemoteService(false)]
        public async Task<List<GetDataNameListDto>> GetNameListAsync(string[] categories)
        {
            List<GetDataNameListDto> dtos = new List<GetDataNameListDto>();
            
            var datas = await _repository.GetListAsync(categories);
            foreach (var category in categories)
            {
                dtos.Add(
                    new GetDataNameListDto()
                    {
                        Category = category,
                        Names = datas.Where(d => d.Category == category).GroupBy(d => d.Name).Select(d => d.Key).ToArray()
                    }
                );
            }

            return dtos;
        }

        /// <summary>
        /// 按类别名称、数据字典名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <returns>数据字典信息列表</returns>
        [RemoteService(false)]
        public async Task<ICollection<string>> GetNameListAsync(string category, string name)
        {
            var datas = await _repository.GetListAsync(category, name);
            return datas.GroupBy(d => d.Remark1).Select(d => d.Key).ToArray();
        }

        /// <summary>
        /// 按类别名称、数据字典备注查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="remark">数据字典备注</param>
        /// <returns>数据字典信息列表</returns>
        [RemoteService(false)]
        public async Task<List<DataDto>> GetListRemarkAsync(string category, string remark)
{
            var datas = await _repository.GetListRemarkAsync(category, remark);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }
    }
}
