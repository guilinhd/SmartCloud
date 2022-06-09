using SmartCloud.Common.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Datas
{
    public interface IDataAppService : ICrudAppService<DataDto, Guid, GetDataListDto, DataDto>
    {
        /// <summary>
        /// 显示数据字典类别
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<Guid, string>> CreateAsync();

        /// <summary>
        /// 显示数据字典类别
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        Task<Dictionary<Guid, string>> CreateAsync(string userName);

        /// <summary>
        /// 按类别名称批量删除
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <returns></returns>
        Task DeleteAsync(string category);

        /// <summary>
        /// 按类别名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<DataDto>> GetListAsync(string category);
    }
}
