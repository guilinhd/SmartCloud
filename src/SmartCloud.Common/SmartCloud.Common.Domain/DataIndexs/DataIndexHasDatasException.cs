using Volo.Abp;

namespace SmartCloud.Common.Domain.DataIndexs 
{
    public class DataIndexHasDatasException : BusinessException
    {
        public DataIndexHasDatasException(string name)
            : base(CommonDomainErrorCodes.DataIndexHasDatas)
        {
            WithData("name", name);
        }
    }
}
