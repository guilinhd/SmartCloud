using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SmartCloud.Core.Organizations
{
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {
        private readonly IOrganizationRepository _repository;
        private readonly OrganizationManager _manager;

        
        public OrganizationAppService(
            IOrganizationRepository repository,
            OrganizationManager manager)
        {
            _repository = repository;
            _manager = manager;
        }

        public async Task<OrganizationDto> CreateAsync(OrganizationDto dto)
        {
            var organization = await _manager.Create(dto.ParentId, dto.No, dto.Name, dto.Type, dto.Phone, dto.Fax, dto.Descriptions);
            await _repository.InsertAsync(organization);

            return ObjectMapper.Map<Organization, OrganizationDto>(organization);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationDto> GetAsync(Guid id)
        {
            var organization = await _repository.GetAsync(id);
            if (organization != null)
            {
                return ObjectMapper.Map<Organization, OrganizationDto>(organization);
            }

            return null;
        }

        public Task<List<OrganizationDto>> GetListAsync(string parentId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Guid id, OrganizationDto dto)
        {
            var organization = await _repository.GetAsync(id);
            if (organization != null)
            {
                organization.No = dto.No;
                organization.Type = dto.Type;
                organization.Phone = dto.Phone;
                organization.Fax = dto.Fax;

                await _manager.ChangeNameAsync(organization, dto.Name, dto.Descriptions);
            }
        }
    }
}
