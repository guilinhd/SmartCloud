using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Core.Organizations
{
    public interface IOrganizationRepository : IRepository<Organization, Guid>
    {
        Task<List<Organization>> GetListAsync(QueryEnum query, string name);
    }
}
