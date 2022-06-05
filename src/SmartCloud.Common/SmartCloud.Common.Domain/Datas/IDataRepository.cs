using SmartCloud.Common.DataIndexs;
using SmartCloud.Common.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Datas
{
    public interface IDataRepository : IRepository<Data, Guid>
    {
        /// <summary>
        /// 按类别名称查询
        /// </summary>
        /// <param name="skipCount">起始行</param>
        /// <param name="maxResultCount">每页行数</param>
        /// <param name="sorting">排序字段</param>
        /// <param name="category">类别名称</param>
        /// <returns></returns>
        Task<List<Data>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string category);

        /// <summary>
        /// 按类别名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<Data>> GetListAsync(string category);

        /// <summary>
        /// 按类别名称批量查询
        /// </summary>
        /// <param name="categoires">类别名称数组</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<Data>> GetListAsync(string[] categoires);

        /// <summary>
        /// 按类别名称、数据字典名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<Data>> GetListAsync(string category, string name);

        /// <summary>
        /// 按类别名称、数据字典名称、数据字典备注查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <param name="remark">数据字典备注</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<Data>> GetListAsync(string category, string name, string remark);

        /// <summary>
        /// 按类别名称、备注查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="remark">备注</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<Data>> GetListRemarkAsync(string category, string remark);
    }
}
