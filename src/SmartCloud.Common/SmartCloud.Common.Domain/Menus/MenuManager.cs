
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
    }
}
