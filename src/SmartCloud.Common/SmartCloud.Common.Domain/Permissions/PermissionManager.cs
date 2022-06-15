using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.Permissions
{
    public class PermissionManager : DomainService
    {
        private readonly IPermissionRepository _repository;

        public PermissionManager(IPermissionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="permissions">实体</param>
        /// <returns></returns>
        public async Task<Permission> CreateAsync(
            [NotNull] Permission permission
        )
        {
            Check.NotNull(permission, nameof(permission));
            return await _repository.InsertAsync(permission);
        }

        /// <summary>
        /// 批量更新存盘
        /// </summary>
        /// <param name="permissions">实体列表</param>
        /// <returns></returns>
        public async Task UpdateAsync([NotNull] List<Permission> permissions)
        {
            Check.NotNull(permissions, nameof(permissions));
            await _repository.UpdateManyAsync(permissions);
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
        /// 查询
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public async Task<List<Permission>> GetListAsync(string userName)
        {
            return await _repository.GetListAsync(userName);    
        }
    }
}
