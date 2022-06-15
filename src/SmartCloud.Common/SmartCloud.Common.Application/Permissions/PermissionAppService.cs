using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Menus;
using SmartCloud.Common.Organizations;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Permissions
{
    public class PermissionAppService : ApplicationService, IPermissionAppService
    {
        private readonly PermissionManager _manager;
        private readonly OrganizationManager _organizationManager;
        private readonly MenuManager _menuManager;

        public PermissionAppService(
            PermissionManager manager,
            OrganizationManager organizationManager,
            MenuManager menuManager
        )
        {
            _manager = manager;
            _organizationManager = organizationManager;
            _menuManager = menuManager;
        }

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public async Task<List<PermissionDto>> CreateAsync(List<PermissionDto> dtos)
        {
            var permissions = new List<Permission>();

            foreach (var dto in dtos)
            {
                var permission = await _manager.CreateAsync(ObjectMapper.Map<PermissionDto, Permission>(dto));
                permissions.Add(permission);
            }

            return ObjectMapper.Map<List<Permission>, List<PermissionDto>>(permissions);
        }

        [HttpGet]
        public async Task<CreatePermissionDto> CreateAsync(string userName)
        {
            var dto = new CreatePermissionDto();

            dto.UserName = userName;

            //菜单列表
            dto.Menus = ObjectMapper.Map<List<Menu>, List<MenuDto>>(await _menuManager.GetListAsync());

            //组织结构列表
            dto.Organizations = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(await _organizationManager.GetListAsync());

            //权限列表
            dto.Permissions = ObjectMapper.Map<List<Permission>, List<PermissionDto>>(await _manager.GetListAsync(userName));

            return dto;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string[] ids)
        {
            await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="dtos">实体列表</param>
        /// <returns></returns>
        public async Task UpdateAsync(List<PermissionDto> dtos)
        {
            var permissions = ObjectMapper.Map<List<PermissionDto>, List<Permission>>(dtos);
            await _manager.UpdateAsync(permissions);
        }
    }
}
