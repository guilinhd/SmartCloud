using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Menus
{
    public interface IMenuAppService : ICrudAppService<MenuDto, Guid>
    {
        Task<List<MenuDto>> GetListAsync();
    }
}
