using Volo.Abp;

namespace SmartCloud.Common.Organizations
{
    public class OrganizationHasChildrenException : BusinessException
    {
        public OrganizationHasChildrenException(string name)
            : base(CommonDomainErrorCodes.OrganizationHasChildren)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "有下级组织结构,不能删除!");
        }
    }
}
