using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.DataIndexs
{
    public interface IDataIndexAppService : IApplicationService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>数据字典类别信息</returns>
        Task<DataIndexDto> CreateAsync(string name);

        /// <summary>
        /// 修改名称
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="input">类别信息</param>
        /// <returns>数据字典类别信息</returns>
        Task<DataIndexDto> UpdateAsync(Guid id, DataIndexDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">类别Id</param>
        /// <returns>类别信息</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>类别信息</returns>
        Task<DataIndexDto> GetAsync(Guid id);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="name">读者姓名</param>
        /// <returns>类别信息列表</returns>
        Task<List<DataIndexDto>> GetListAsync(string name);
    }
}
