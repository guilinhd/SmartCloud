using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.RoleUsers
{
    public class RoleUserManager
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
    }
}
