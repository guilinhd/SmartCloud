
using SmartCloud.Common.DataIndexs;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.Roles
{
    public class RoleManager : DomainService
    {
        private readonly IRoleRepository _repository;

        public RoleManager(IRoleRepository repository)
        {
            _repository = repository;
        }


        public async Task<Role> CreateAsync(
            [NotNull] string name
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingRole = await _repository.GetAsync(name);
            if (existingRole != null)
            {
                throw new RoleAlreadyExistsException(name);
            }

            var role = new Role(
                GuidGenerator.Create(),
                name
            );

            //新增role
            await _repository.InsertAsync(role);
            return role;
        }

        

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role">实体</param>
        /// <returns></returns>
        public async Task DeleteAsync(Role role)
        {
            await _repository.DeleteAsync(role);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="role">实体</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        /// <exception cref="RoleAlreadyExistsException"></exception>
        public async Task UpdateAsync(
            [NotNull] Role role,
            [NotNull] string name
        )
        {
            Check.NotNull(role, nameof(role));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingRole = await _repository.GetAsync(name);
            if (existingRole != null && existingRole.Id != role.Id)
            {
                throw new RoleAlreadyExistsException(name);
            }

            role.ChangeName(name);
            await _repository.UpdateAsync(role);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<Role>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }
    }
}
