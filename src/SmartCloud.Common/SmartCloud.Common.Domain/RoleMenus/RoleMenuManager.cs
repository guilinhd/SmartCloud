
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.RoleMenus
{
    public class RoleMenuManager : DomainService
    {
        private readonly IRoleMenuRepository _repository;

        public RoleMenuManager(
            IRoleMenuRepository repository
        )
        {
            _repository = repository;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menuIds">菜单ids</param>
        /// <returns></returns>
        public async Task<List<RoleMenu>> CreateAsync(string roleId, string[] menuIds)
        {
            var roleMenus = new List<RoleMenu>();
            foreach (var menuId in menuIds)
            {
                roleMenus.Add(new RoleMenu()
                {
                    RoleId = roleId,
                    MenuId = menuId
                });
            }
            await _repository.InsertManyAsync(roleMenus);

            return roleMenus;
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
            var roleMenus = await _repository.GetListAsync(QueryEnum.RoleId, roleId);
            await _repository.DeleteManyAsync(roleMenus);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<List<RoleMenu>> GetListAsync(string roleId)
        {
            return await _repository.GetListAsync(QueryEnum.RoleId, roleId);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleMenu>> GetListAsync()
        {
            return await _repository.GetListAsync(QueryEnum.All, "");
        }
    }
}
