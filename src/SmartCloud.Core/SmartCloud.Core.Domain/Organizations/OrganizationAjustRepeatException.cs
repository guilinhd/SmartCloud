using Volo.Abp;

namespace SmartCloud.Core.Organizations
{
    public class OrganizationAjustRepeatException : BusinessException
    {
        public OrganizationAjustRepeatException(string name)
            : base(CoreDomainErrorCodes.OrganizationAjustRepeat)
        {
            WithData("name", name);
            WithData("category", "业务数据错误");
            WithData("reason", "组织结构相同,调整失败!");
        }
    }
}
