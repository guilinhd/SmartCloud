
using SmartCloud.Common.RoleUsers;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Users
{
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns>实体</returns>
        Task<SaveUserDto> CreateAsync(CreateUpdateUserDto dto);

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        Task<CreateUserDto> CreateAsync();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SaveUserDto> GetAsync(Guid id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        Task<FullUserDto> GetAsync(string name, string pwd);

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task PwdResetAsync(Guid id);

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        Task PwdChangeAsync(ChangeUserPwdDto dto);

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task UpdateAsync(Guid id);

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="userDto">实体</param>
        /// <returns>roleUseIds</returns>
        Task<SaveUserDto> UpdateAsync(Guid id, CreateUpdateUserDto dto);
    }
}
