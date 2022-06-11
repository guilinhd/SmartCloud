using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Menus
{
    public interface IMenuAppService : ICrudAppService<MenuDto, Guid>
    {
        /// <summary>
        /// 调整菜单到新的上级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId">上级菜单Id</param>
        /// <returns></returns>
        Task AdjustAsync(Guid id, string parentId);

        Task<List<MenuDto>> GetListAsync();
    }
}
