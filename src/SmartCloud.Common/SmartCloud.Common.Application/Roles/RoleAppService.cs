using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Roles
{
    public class RoleAppService : CrudAppService<Role, RoleDto, Guid>, IRoleAppService
    {
        private readonly IRepository<Role, Guid> _repository;
        private readonly RoleManager _manager;

        public RoleAppService(
            IRepository<Role, Guid> repository,
            RoleManager manager
        ) : base(repository)
        {
            _repository = repository;
            _manager = manager;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteAsync(Guid id)
        {
            var role = await _repository.GetAsync(id);

            await _manager.DeleteAsync(role);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetListAsync()
        {
            var roles = await _repository.GetListAsync();
            return ObjectMapper.Map<List<Role>, List<RoleDto>>(roles);
        }

        [RemoteService(false)]
        public override Task<PagedResultDto<RoleDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
    }
}
