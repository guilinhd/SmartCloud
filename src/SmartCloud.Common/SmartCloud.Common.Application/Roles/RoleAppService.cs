using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Menus;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.RoleUsers;
using SmartCloud.Common.Users;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Roles
{
    public class RoleAppService : ApplicationService, IRoleAppService
    {
        private readonly IRoleRepository _repository;
        private readonly RoleManager _manager;
        private readonly IOrganizationAppService _organizationAppService;
        private readonly IMenuAppService _menuAppService;
        private readonly RoleUserManager _roleUserManager;
        private readonly RoleMenuManager _roleMenuManager;
        private readonly DataManager _datatManager;
        private readonly IUserAppService _userAppService;

        public RoleAppService(
            IRoleRepository repository,
            RoleManager manager,
            IOrganizationAppService organizationAppService,
            IMenuAppService menuAppService,
            RoleUserManager roleUserManager,
            RoleMenuManager roleMenuManager,
            DataManager datatManager
        ) 
        {
            _repository = repository;
            _manager = manager;
            _organizationAppService = organizationAppService;
            _menuAppService = menuAppService;
            _roleUserManager = roleUserManager;
            _roleMenuManager = roleMenuManager;
            _datatManager = datatManager;
        }

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public async Task<SaveRoleDto> CreateAsync(CreateSaveRoleDto dto)
        {
            var role = await _manager.CreateAsync(dto.Name);
            var roleUsers = await _roleUserManager.CreateAsync(role.Id.ToString(), dto.UserIds);
            var roleMenus = await _roleMenuManager.CreateAsync(role.Id.ToString(), dto.MenuIds);

            return new SaveRoleDto()
            {
                Id = role.Id,
                Name = dto.Name,
                Users = ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(roleUsers),
                Menus = ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(roleMenus)
            };
        }

        [HttpGet]
        public async Task<CreateRoleDto> CreateAsync()
        {
            var dto = new CreateRoleDto();

            //组织结构
            dto.Organization = await _organizationAppService.GetNodeAsync();

            //dto.Users = createUserDto.Users;

            var datas = await _datatManager.GetNameListAsync(new string[] { "职务列表" });
            dto.Datas = datas.First().Value;

            dto.Roles = ObjectMapper.Map<List<Role>, List<RoleDto>>(await _repository.GetListAsync());

            //菜单列表
            dto.Menu = await _menuAppService.GetNodeAsync();

            return dto;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var role = await _repository.GetAsync(id);

            //角色
            await _manager.DeleteAsync(role);
            //角色用户
            await _roleUserManager.DeleteAsync(id.ToString());
            //角色菜单
            await _roleMenuManager.DeleteAsync(id.ToString());
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<SaveRoleDto> GetAsync(Guid id)
        {
            var dto = ObjectMapper.Map<Role, SaveRoleDto>(await _repository.GetAsync(id));

            dto.Users = ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(await _roleUserManager.GetListAsync(RoleUsers.QueryEnum.RoleId, id.ToString()));
            dto.Menus = ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(await _roleMenuManager.GetListAsync(RoleMenus.QueryEnum.RoleId, id.ToString()));

            return dto;
        }

        /// <summary>
        /// 修改存盘
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>

        public async Task<SaveRoleDto> UpdateAsync(UpdateSaveRoleDto dto)
        {
            var role = await _repository.GetAsync(dto.Id);
            //修改存盘角色
            await _manager.UpdateAsync(role, dto.Name);

            //新增角色用户
            var roleUsers = await _roleUserManager.CreateAsync(role.Id.ToString(), dto.UserIds);
            //删除角色用户
            await _roleUserManager.DeleteAsync(dto.RoleUserIds);

            //新增角色菜单
            var roleMenus = await _roleMenuManager.CreateAsync(role.Id.ToString(), dto.MenuIds);
            //删除角色菜单
            await _roleMenuManager.DeleteAsync(dto.RoleMenuIds);

            return new SaveRoleDto()
            {
                Name = dto.Name,
                Users = ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(roleUsers),
                Menus = ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(roleMenus)
            };
        }
    }
}
