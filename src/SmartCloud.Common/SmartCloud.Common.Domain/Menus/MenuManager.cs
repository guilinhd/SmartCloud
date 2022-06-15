
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.Menus
{
    public class MenuManager : DomainService
    {
        private readonly IMenuRepository _repository;

        public MenuManager(IMenuRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 调整至新的上级菜单
        /// </summary>
        /// <param name="menu">实体</param>
        /// <param name="menuParent">上级实体</param>
        /// <returns></returns>
        public async Task AdjustAsync(
            [NotNull] Menu menu, 
            [NotNull] Menu menuParent)
        {
            Check.NotNull(menu, nameof(menu));  
            Check.NotNull(menuParent, nameof(menuParent));

            menu.Category = menuParent.Category + 1;
            menu.ParentId = menuParent.ParentId;

            await _repository.UpdateAsync(menu);
        }

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="menu">实体</param>
        /// <returns></returns>
        public async Task<Menu> CreateAsync([NotNull] Menu menu)
        {
            Check.NotNull(menu, nameof(menu));

            return await _repository.InsertAsync(menu);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menu">实体</param>
        /// <returns></returns>
        public async Task DeleteAsync([NotNull] Menu menu)
        {
            Check.NotNull(menu, nameof(menu));
            await _repository.DeleteAsync(menu);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<Menu>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        /// <summary>
        /// 修改存盘
        /// </summary>
        /// <param name="menu">实体</param>
        /// <returns></returns>
        public async Task UpdateAsync([NotNull] Menu menu)
        {
            Check.NotNull(menu, nameof(menu));
            await _repository.UpdateAsync(menu);
        }
    }
}
