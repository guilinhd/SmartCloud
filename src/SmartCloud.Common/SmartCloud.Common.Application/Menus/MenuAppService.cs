using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Menus
{
    public class MenuAppService : CrudAppService<Menu, MenuDto, Guid>, IMenuAppService
    {
        private readonly IMenuRepository _repository;
        private readonly MenuManager _manager;

        public MenuAppService(
            IMenuRepository repository,
            MenuManager manager
        ) : base(repository)
        {
            _repository = repository;
            _manager = manager;
        }

        /// <summary>
        /// 调整菜单到新的上级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task AdjutAsync(Guid id, string parentId)
        {
            var menu = await _repository.GetAsync(id);
            var menuParent = await _repository.GetAsync(new Guid(parentId));

            await _manager.AdjustAsync(menu, menuParent);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetListAsync()
        {
            var menus = await _repository.GetListAsync(QueryEnum.All, "");
            return ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus);
        }

        [RemoteService(false)]
        public override Task<PagedResultDto<MenuDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
    }
}
