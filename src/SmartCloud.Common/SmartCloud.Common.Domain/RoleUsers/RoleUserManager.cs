using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.RoleUsers
{
    public class RoleUserManager : DomainService
    {
        private readonly IRoleUserRepository _repository;

        public RoleUserManager(
            IRoleUserRepository repository
        )
        {
            _repository = repository;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="userIds">用户id</param>
        /// <returns></returns>
        public async Task<List<RoleUser>> CreateAsync(string roleId, string[] userIds)
        {
            var roleUsers = new List<RoleUser>();
            foreach (var userId in userIds)
            {
                roleUsers.Add(new RoleUser() { 
                    RoleId = roleId,
                    UserId = userId
                });
            }
            await _repository.InsertManyAsync(roleUsers);

            return roleUsers;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string[] ids)
        {
            Guid[] guids = new Guid[ids.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                guids[i] = new Guid(ids[i]);
            }

            await _repository.DeleteManyAsync(guids);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task DeleteAsync(string roleId)
        {
            var roleUsers = await _repository.GetListAsync(QueryEnum.RoleId, roleId);
            await _repository.DeleteManyAsync(roleUsers);
        }

        /// <summary>
        /// 批量查询
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<List<RoleUser>> GetListAsync(string roleId)
        {
            return await _repository.GetListAsync(QueryEnum.RoleId, roleId);
        }
    }
}
