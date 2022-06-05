using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCloud.Common.Datas
{
    public class GetDataNameListDto
    {
        public string Category { get; set; }

        public ICollection<string> Names { get; set; }
    }
}
