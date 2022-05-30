using Volo.Abp;

namespace SmartCloud.Common.DataIndexs 
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
