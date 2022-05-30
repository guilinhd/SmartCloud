using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.DataIndexs
{
    public class DataIndexDto : AuditedEntityDto<Guid>
    {
        public string Name { set; get; }

        public string Reader { set; get; }

        public string Editor { set; get; }

        public List<Description> Descriptions { get; set; }
    }
}
