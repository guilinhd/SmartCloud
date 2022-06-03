using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace SmartCloud.Core.Organizations
{
    public class OrganizationAlreadyExistsException : BusinessException
    {
       public OrganizationAlreadyExistsException(string name)
            : base(CoreDomainErrorCodes.OrganizationAlreadyExists)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "组织结构名称重复!");
        }
    }
}
