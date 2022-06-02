
using Volo.Abp.Domain.Services;

namespace SmartCloud.Core.Organizations
{
    public class OrganizationManager : DomainService
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationManager(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 调整组织结构的所属上级
        /// </summary>
        /// <param name="id">组织结构id</param>
        /// <param name="category">在组织结构所在的层级</param>
        /// <param name="parentId">上级组织结构id</param>
        /// <returns></returns>
        public async Task Adjust(Guid id, int category, string parentId)
        {
            var organization = await _repository.GetAsync(id);
            if (organization != null)
            {
                organization.Category = category;
                organization.ParentId = parentId;

                await _repository.UpdateAsync(organization);
            }
        }
    }
}
