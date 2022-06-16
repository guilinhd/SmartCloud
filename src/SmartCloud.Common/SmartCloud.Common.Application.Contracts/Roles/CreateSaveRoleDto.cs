
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Roles
{
    public class CreateSaveRoleDto : RoleDto
    {
        /// <summary>
        /// 新增用户Ids
        /// </summary>
        public string[] UserIds { get; set; }

        /// <summary>
        /// 新增菜单Ids
        /// </summary>
        public string[] MenuIds { get; set; }   
    }
}
