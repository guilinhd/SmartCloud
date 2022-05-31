using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace SmartCloud.Common.DataIndexs 
{
    public class DataIndexAlreadyExistsException : BusinessException
    {
        public DataIndexAlreadyExistsException(string name)
            : base(CommonDomainErrorCodes.DataIndexAlreadyExists)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "类别名称重复!");
        }

        
    }
}
