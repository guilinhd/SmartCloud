using Volo.Abp;

namespace SmartCloud.Common.Users
{
    public class UserPwdInvalidException : BusinessException
    {
        public UserPwdInvalidException(string name)
            :base(CommonDomainErrorCodes.UserPwdInvalid)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "用户名或密码不正确!");
        }
    }
}
