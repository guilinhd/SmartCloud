using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Menus;
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
        private readonly MenuManager _menuManager;
        private readonly RoleUserManager _roleUserManager;
        private readonly RoleMenuManager _roleMenuManager;
        private readonly IUserAppService _userAppService;

        public RoleAppService(
            IRoleRepository repository,
            RoleManager manager,
            MenuManager  menuManager,
            RoleUserManager roleUserManager,
            RoleMenuManager roleMenuManager,
            IUserAppService userAppService
        ) 
        {
            _repository = repository;
            _manager = manager;
            _menuManager = menuManager;
            _roleUserManager = roleUserManager;
            _roleMenuManager = roleMenuManager;
            _userAppService = userAppService;
        }

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public async Task<RoleDto> CreateAsync(CreateUpdateRoleDto dto)
        {
            var role = await _manager.CreateAsync(dto.Name);
            var roleUsers = await _roleUserManager.CreateAsync(role.Id.ToString(), dto.RoleUserIds);
            var roleMenus = await _roleMenuManager.CreateAsync(role.Id.ToString(), dto.RoleMenuIds);

            return new RoleDto()
            {
                Name = dto.Name,
                Users = ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(roleUsers),
                Menus = ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(roleMenus)
            };
        }

        [HttpGet]
        public async Task<CreateRoleDto> CreateAsync()
        {
            var dto = new CreateRoleDto();

            var createUserDto = await _userAppService.CreateAsync();
            dto.Organizations = createUserDto.Organizations;
            dto.Users = createUserDto.Users;
            dto.Datas = createUserDto.Datas;

            dto.Roles = ObjectMapper.Map<List<Role>, List<PartRoleDto>>(await _repository.GetListAsync());
            dto.Menus = ObjectMapper.Map<List<Menu>, List<MenuDto>>(await _menuManager.GetListAsync());

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
        public async Task<RoleDto> GetAsync(Guid id)
        {
            var dto = ObjectMapper.Map<Role, RoleDto>(await _repository.GetAsync(id));

            dto.Users = ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(await _roleUserManager.GetListAsync(RoleUsers.QueryEnum.RoleId, id.ToString()));
            dto.Menus = ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(await _roleMenuManager.GetListAsync(RoleMenus.QueryEnum.RoleId, id.ToString()));

            return dto;
        }

        /// <summary>
        /// 修改存盘
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>

        public async Task<RoleDto> UpdateAsync(CreateUpdateRoleDto dto)
        {
            var role = await _repository.GetAsync(dto.Id);
            //修改存盘角色
            await _manager.UpdateAsync(role, dto.Name);

            //新增角色用户
            var roleUsers = await _roleUserManager.CreateAsync(role.Id.ToString(), dto.RoleUserIds);
            //删除角色用户
            await _roleUserManager.DeleteAsync(dto.RoleUserIds);

            //新增角色菜单
            var roleMenus = await _roleMenuManager.CreateAsync(role.Id.ToString(), dto.RoleMenuIds);
            //删除角色菜单
            await _roleMenuManager.DeleteAsync(dto.RoleMenuIds);

            return new RoleDto()
            {
                Name = dto.Name,
                Users = ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(roleUsers),
                Menus = ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(roleMenus)
            };
        }
    }
}
