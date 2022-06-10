using Volo.Abp;

namespace SmartCloud.Common.Users
{
    public class UserAlreadyExistsException : BusinessException
    {
        public UserAlreadyExistsException(string name)
            :base(CommonDomainErrorCodes.UserAlreadyExists)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "用户名称重复!");
        }
    }
}
