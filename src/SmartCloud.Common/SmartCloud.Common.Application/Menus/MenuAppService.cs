using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Menus
{
    public class MenuAppService : CrudAppService<Menu, MenuDto, Guid>, IMenuAppService
    {
        private readonly IMenuRepository _repository;

        public MenuAppService(IMenuRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<MenuDto>> GetListAsync()
        {
            var menus = await _repository.GetListAsync(QueryEnum.All, "");
            return ObjectMapper.Map<List<Menu>, List<MenuDto>>(menus);
        }
    }
}
