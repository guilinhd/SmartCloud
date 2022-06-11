using Volo.Abp.Application.Services;

namespace SmartCloud.Common.RoleUsers
{
    public class RoleUserAppService : ApplicationService, IRoleUserAppService
    {
        private readonly IRoleUserRepository _repository;
        private readonly RoleUserManager _manager;

        public RoleUserAppService(
            IRoleUserRepository repository,
            RoleUserManager manager
        )
        {
            _repository = repository;
            _manager = manager;
        }

        public async Task<List<RoleUserDto>> CreateAsync(CreateRoleUserDto dto)
        {
            string roleId = dto.RoleId;

            //批量删除
            await _manager.DeleteAsync(dto.Ids);

            var roleUsers = await _manager.CreateAsync(roleId, dto.UserIds);
            return ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(roleUsers);
        }

        public async Task<List<RoleUserDto>> GetListAsync(string roleId)
        {
            var roleUsers = await  _repository.GetListAsync(QueryEnum.RoleId, roleId);
            return ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(roleUsers);
        }
    }
}
