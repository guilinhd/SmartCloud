using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.Roles;
using SmartCloud.Common.RoleUsers;
using SmartCloud.Common.Users;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Menus
{
    public class MenuAppService :  ApplicationService , IMenuAppService
    {
        private readonly IMenuRepository _repository;
        private readonly MenuManager _manager;
        private readonly RoleManager _roleManager;
        private readonly RoleMenuManager _roleMenuManager;
        private readonly RoleUserManager _roleUserManager;
        private readonly IOrganizationAppService _organizationAppService;
        private readonly UserManager _userManager;

        public MenuAppService(
            IMenuRepository repository,
            MenuManager manager,
            RoleManager roleManager,
            RoleMenuManager roleMenuManager,
            RoleUserManager roleUserManager,
            IOrganizationAppService organizationAppService,
            UserManager userManager
        ) 
        {
            _repository = repository;
            _manager = manager;
            _roleManager = roleManager;
            _roleMenuManager = roleMenuManager;
            _roleUserManager = roleUserManager;
            _organizationAppService = organizationAppService;
            _userManager = userManager;
        }

        /// <summary>
        /// 调整菜单到新的上级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task AdjustAsync(Guid id, string parentId)
        {
            var menu = await _repository.GetAsync(id);
            var menuParent = await _repository.GetAsync(new Guid(parentId));

            await _manager.AdjustAsync(menu, menuParent);
        }

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<SaveMenuDto> CreateAsync(CreateSaveMenuDto dto)
        {
            //新增存盘
            var menu = await _manager.CreateAsync(ObjectMapper.Map<CreateSaveMenuDto, Menu>(dto));

            var saveMenuDto = ObjectMapper.Map<Menu, SaveMenuDto>(menu);

            #region 角色菜单存盘
            var roleMenus = new List<RoleMenu>();

            foreach (var roleId in dto.Roles)
            {
                var result = await _roleMenuManager.CreateAsync(roleId, new string[] { menu.Id.ToString() });
                saveMenuDto.RoleMenus.Add(ObjectMapper.Map<RoleMenu, RoleMenuDto>(result.First()));
            }
            #endregion

            //角色人员
            saveMenuDto.Users = await GetRoleUsersAsync(dto.Roles);

            return saveMenuDto;
        }

        /// <summary>
        /// 新增初始化
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<CreateMenuDto> CreateAsync()
        {
            var dto = new CreateMenuDto();

            dto.Organization = await _organizationAppService.GetNodeAsync();
            dto.Menu = await GetNodeAsync();

            dto.Users = ObjectMapper.Map<List<User>, List<PartUserDto>>(await _userManager.GetListAsync());

            //角色列表
            var roles = await _roleManager.GetListAsync();
            dto.Roles = new();
            foreach (var role in roles)
            {
                dto.Roles.Add(role.Id, role.Name);
            }

            return dto;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteAsync(Guid id)
        {
            var menu = await _repository.GetAsync(id);

            await _manager.DeleteAsync(menu);

            //获取rolemenu
            var roleMenus = await _roleMenuManager.GetListAsync(RoleMenus.QueryEnum.MenuId, id.ToString());
            var ids = roleMenus.Select(d => d.Id.ToString()).ToArray();
            await _roleMenuManager.DeleteAsync(ids);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<SaveMenuDto> GetAsync(Guid id)
        {
            var menu = await _repository.GetAsync(id);
            SaveMenuDto dto = ObjectMapper.Map<Menu, SaveMenuDto>(menu);

            //角色列表
            var roleMenus = await _roleMenuManager.GetListAsync(RoleMenus.QueryEnum.MenuId, id.ToString());
            dto.RoleMenus = ObjectMapper.Map<List<RoleMenu>, List<RoleMenuDto>>(roleMenus);


            //角色人员列表
            var roleIds = roleMenus.Select(d => d.Id.ToString()).ToArray();
            dto.Users = await GetRoleUsersAsync(roleIds);

            return dto;
        }

        /// <summary>
        /// 修改存盘
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<SaveMenuDto> UpdateAsync(Guid id, UpdateSaveMenuDto dto)
        {
            var menu = await _repository.GetAsync(id);

            menu.No = dto.No;
            menu.Name = dto.Name;
            menu.Href = dto.Href;
            menu.ApiName = dto.ApiName;
            menu.Method = dto.Method;
            menu.ImageName = dto.ImageName;
            menu.Type = dto.Type;

            await _manager.UpdateAsync(menu);
            var saveMenuDto = ObjectMapper.Map<Menu, SaveMenuDto>(menu);

            #region 角色菜单存盘
            foreach (var roleId in dto.Roles)
            {
                var result = await _roleMenuManager.CreateAsync(roleId, new string[] { menu.Id.ToString() });
                saveMenuDto.RoleMenus.Add(ObjectMapper.Map<RoleMenu, RoleMenuDto>(result.First()));
            }

            #endregion

            #region 角色菜单删除
            await _roleMenuManager.DeleteAsync(dto.RoleMenus);
            #endregion

            //角色人员
            saveMenuDto.Users = await GetRoleUsersAsync(dto.Roles);

            return saveMenuDto;
        }

        /// <summary>
        /// 获取菜单Tree
        /// </summary>
        /// <returns></returns>
        [RemoteService(false)]
        public async Task<INodeDto> GetNodeAsync()
        {
            var menus = await _manager.GetListAsync();

            INodeDto root = new NodeDto("功能菜单列表");
            root.ToTree(ObjectMapper.Map<List<Menu>, List<INodeDto>>(menus));

            return root;
        }

        /// <summary>
        /// 按角色id查询用户id
        /// </summary>
        /// <param name="roleIds">角色id</param>
        /// <returns></returns>
        private async Task<string[]> GetRoleUsersAsync(string[] roleIds)
        {
            var roleUsers = new List<RoleUser>();
            foreach (var roleId in roleIds)
            {
                roleUsers.AddRange(await _roleUserManager.GetListAsync(RoleUsers.QueryEnum.RoleId, roleId));
            }

            return roleUsers.Select(d => d.UserId).ToArray();   
        }
    }
}
