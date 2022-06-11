
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.Roles
{
    public class RoleManager : DomainService
    {
        private readonly IRepository<Role, Guid> _repository;

        public RoleManager(IRepository<Role, Guid> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role">实体</param>
        /// <returns></returns>
        public async Task DeleteAsync(Role role)
        {
            //删除rolemenu

            //删除roleuser

            await _repository.DeleteAsync(role);
        }
    }
}
