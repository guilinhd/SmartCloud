using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Common.Organizations
{
    public interface IOrganizationRepository : IRepository<Organization, Guid>
    {
        Task<List<Organization>> GetListAsync(QueryEnum query, string name);
    }
}
