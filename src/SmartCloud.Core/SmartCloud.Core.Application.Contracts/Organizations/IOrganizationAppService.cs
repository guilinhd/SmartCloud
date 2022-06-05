using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Core.Organizations
{
    public interface IOrganizationAppService : ICrudAppService<OrganizationDto, Guid>
    {
        Task<List<OrganizationDto>> GetListAsync(QueryEnum query, string name);
    }
}
