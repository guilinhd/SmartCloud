using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Menus
{
    public interface IMenuAppService : ICreateUpdateAppService<MenuDto, Guid>
    {
        Task<List<MenuDto>> GetListAsync();
    }
}
