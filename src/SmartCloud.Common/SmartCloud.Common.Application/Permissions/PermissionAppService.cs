using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Menus;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.Users;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Permissions
{
    public class PermissionAppService : ApplicationService, IPermissionAppService
    {
        private readonly PermissionManager _manager;
        private readonly IOrganizationAppService _organizationAppService;
        private readonly IMenuAppService _menuAppService;

        public PermissionAppService(
            PermissionManager manager,
            IOrganizationAppService organizationAppService,
            IMenuAppService menuAppService
        )
        {
            _manager = manager;
            _organizationAppService = organizationAppService;
            _menuAppService = menuAppService;
        }

        [HttpGet]
        [Route("api/common/permission/create/{userName}")]
        public async Task<CreatePermissionDto> CreateAsync(string userName)
        {
            var dto = new CreatePermissionDto();

            dto.UserName = userName;

            //菜单列表
            dto.Menu = await _menuAppService.GetNodeAsync();

            //组织结构列表
            dto.Organization = await _organizationAppService.GetNodeAsync();

            //权限列表
            dto.Permissions = ObjectMapper.Map<List<Permission>, List<PermissionDto>>(await _manager.GetListAsync(userName));

            return dto;
        }

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/common/permission/save")]
        public async Task SaveAsync(SavePermissionDto dto)
        {
            #region 新增存盘
            if (dto.NewPermissions != null)
            {
                var newPermissions = ObjectMapper.Map<List<PermissionDto>, List<Permission>>(dto.NewPermissions);
                await _manager.CreateAsync(newPermissions);
            }
            #endregion

            #region 修改存盘
            if (dto.UpdatePermissions != null)
            {
                IEnumerable<Guid> ids = dto.UpdatePermissions.Select(t => t.Id);
                List<Permission> permissions = await _manager.GetListAsync(ids);
                UpdatePermissionDto newPermission;
                foreach (var permission in permissions)
                {
                    newPermission = dto.UpdatePermissions.Find(t => t.Id == permission.Id);
                    if (newPermission != null)
                    {
                        permission.Status = newPermission.Status;
                        //permission.ConcurrencyStamp = newPermission.ConcurrencyStamp;
                    }
                }
                await _manager.UpdateAsync(permissions);
            }
            #endregion

            #region 删除
            if (dto.DeletePermissions != null)
            {
                await _manager.DeleteAsync(dto.DeletePermissions.Where(t => !string.IsNullOrEmpty(t)).ToArray());
            }
            #endregion
        }



        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        [RemoteService(false)]
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

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [RemoteService(false)]
        public async Task DeleteAsync(string[] ids)
        {
            await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="dtos">实体列表</param>
        /// <returns></returns>
        [RemoteService(false)]
        public async Task UpdateAsync(List<PermissionDto> dtos)
        {
            var permissions = ObjectMapper.Map<List<PermissionDto>, List<Permission>>(dtos);
            await _manager.UpdateAsync(permissions);
        }

        /// <summary>
        /// 按id更新
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/common/permission/updateOne")]
        [RemoteService(false)]
        public async Task UpdateAsync(PermissionDto dto)
        {
            Permission newEntity = ObjectMapper.Map<PermissionDto, Permission>(dto);
            await _manager.UpdateAsync(newEntity);
        }
    }
}
