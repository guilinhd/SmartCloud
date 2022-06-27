using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Permissions
{
    public class SavePermissionDto
    {
        /// <summary>
        /// 新增权限列表
        /// </summary>
        public List<PermissionDto> NewPermissions { get; set; }

        /// <summary>
        /// 修改权限列表
        /// </summary>
        public List<UpdatePermissionDto> UpdatePermissions { get; set; }

        /// <summary>
        /// 删除权限ids
        /// </summary>
        public List<string> DeletePermissions { get; set; }

    }
}
