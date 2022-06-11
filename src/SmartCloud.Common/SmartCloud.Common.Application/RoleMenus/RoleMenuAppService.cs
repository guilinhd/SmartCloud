

using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.RoleUsers;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.RoleMenus
{
    public class RoleMenuAppService : ApplicationService, IRoleMenuAppService
    {
        private readonly IRoleMenuRepository _repository;
        private readonly RoleMenuManager _manager;

        public RoleMenuAppService(
            IRoleMenuRepository repository,
            RoleMenuManager manager
        )
        {
            _repository = repository;
            _manager = manager;
        }

        [Route("api/common/rolemenu")]
        public async Task<List<RoleMenuDto>> CreateAsync(CreateRoleMenuDto dto)
        {
            string roleId = dto.RoleId;

            //批量删除
            await _manager.DeleteAsync(dto.Ids);

            var roleMenus = await _manager.CreateAsync(roleId, dto.MenuIds);
            return ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(roleMenus);
        }

        [Route("api/common/rolemenu")]
        public async Task<List<RoleMenuDto>> GetListAsync(string roleId)
        {
            var roleMenus = await _repository.GetListAsync(QueryEnum.RoleId, roleId);
            return ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(roleMenus);
        }
    }
}
