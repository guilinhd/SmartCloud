using Volo.Abp;

namespace SmartCloud.Common.Organizations
{
    public class OrganizationHasUsersException : BusinessException
    {
        public OrganizationHasUsersException(string name)
            : base(CommonDomainErrorCodes.OrganizationHasUsers)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "有人员信息,不能删除!");
        }
    }
}
