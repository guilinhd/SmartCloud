using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.RoleMenus
{
    public class CreateRoleMenuDto
    {
        [Required]
        public string RoleId { get; set; }

        /// <summary>
        /// 角色包含菜单id数组
        /// </summary>
        public string MenuIds { get; set; }

        /// <summary>
        /// 需要删除id数组
        /// </summary>
        public string[] Ids { get; set; }
    }
}
