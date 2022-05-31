using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;
using Volo.Abp;

namespace SmartCloud.Common.DataIndexs 
{
    public class DataIndexHasDatasException : BusinessException
    {
        public DataIndexHasDatasException(string name)
            : base(CommonDomainErrorCodes.DataIndexHasDatas)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "当前类别有数据字典信息,不允许删除!");
        }
    }
}
