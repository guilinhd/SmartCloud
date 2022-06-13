using Volo.Abp;

namespace SmartCloud.Common.Roles
{
    public class RoleAlreadyExistsException : BusinessException
    {
        public RoleAlreadyExistsException(string name)
            :base(CommonDomainErrorCodes.RoleAlreadyExists)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "角色名称重复!");
        }
    }
}
