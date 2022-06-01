using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.DataIndexs
{
    /// <summary>
    /// 修改读者、修改者dto
    /// </summary>
    public class AuthorityDto
    {
        /// <summary>
        /// 读者集合
        /// </summary>
        public ICollection<string> Readers { get; set; }

        /// <summary>
        /// 修改者集合
        /// </summary>
        public ICollection<string> Editors { get; set; }
    }
}
