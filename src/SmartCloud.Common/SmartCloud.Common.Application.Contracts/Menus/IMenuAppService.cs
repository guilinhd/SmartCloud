using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Menus
{
    public interface IMenuAppService : IApplicationService
    {
        /// <summary>
        /// 调整菜单到新的上级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId">上级菜单Id</param>
        /// <returns></returns>
        Task AdjustAsync(Guid id, string parentId);

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<SaveMenuDto> CreateAsync(CreateUpdateMenuDto dto);

        /// <summary>
        /// 新增初始化
        /// </summary>
        /// <returns></returns>
        Task<CreateMenuDto> CreateAsync();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 修改存盘
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<SaveMenuDto> UpdateAsync(Guid id, CreateUpdateMenuDto dto);
    }
}
