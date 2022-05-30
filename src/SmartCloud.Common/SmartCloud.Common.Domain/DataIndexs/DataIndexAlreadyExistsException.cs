using Volo.Abp;

namespace SmartCloud.Common.Domain.DataIndexs 
{
    public class DataIndexAlreadyExistsException : BusinessException
    {
        public DataIndexAlreadyExistsException(string name)
            : base(CommonDomainErrorCodes.DataIndexAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
