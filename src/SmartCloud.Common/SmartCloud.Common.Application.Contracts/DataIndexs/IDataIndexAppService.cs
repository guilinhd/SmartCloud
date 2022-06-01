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
        /// <param name="name">名称</param>
        /// <returns>数据字典类别信息</returns>
        Task<DataIndexDto> UpdateAsync(Guid id, string name);

        /// <summary>
        /// 修改描述
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="descriptions">描述信息</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, List<Description> descriptions);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="authority">权限</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, AuthorityDto authority);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">类别Id</param>
        /// <returns>类别信息</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>类别信息</returns>
        Task<DataIndexDto> GetAsync(Guid id);

        /// <summary>
        /// 显示当前读者可以查看的信息列表
        /// </summary>
        /// <param name="name">读者姓名</param>
        /// <returns>类别信息列表</returns>
        Task<List<DataIndexDto>> GetListAsync(string name);
    }
}
