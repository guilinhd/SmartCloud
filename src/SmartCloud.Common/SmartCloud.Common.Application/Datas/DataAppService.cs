using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
        [Route("api/common/data/category/{category}/name/{name}")]
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
        [Route("api/common/data/category/{category}/name/{name}/remark/{remark}")]
        public async Task<List<DataDto>> GetListAsync(string category, string name, string remark)
        {
            var datas = await _repository.GetListAsync(category, name, remark);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }

        /// <summary>
        /// 按类别名称、数据字典备注查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="remark">数据字典备注</param>
        /// <returns>数据字典信息列表</returns>
        [Route("api/common/data/category/{category}/remark/{remark}")]
        public async Task<List<DataDto>> GetListRemarkAsync(string category, string remark)
{
            var datas = await _repository.GetListRemarkAsync(category, remark);
            return ObjectMapper.Map<List<Data>, List<DataDto>>(datas);
        }
    }
}
