using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Permissions
{
    public class UpdatePermissionDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 1-读  2-编辑
        /// </summary>
        public int Status { get; set; }

        //public string ConcurrencyStamp { get; set; }

    }
}
