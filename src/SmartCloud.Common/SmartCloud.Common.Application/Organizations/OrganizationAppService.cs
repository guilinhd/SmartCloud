﻿using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Datas;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Organizations
{
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {
        private readonly IOrganizationRepository _repository;
        private readonly OrganizationManager _manager;
        private readonly IDataAppService _dataAppService;

        public OrganizationAppService(
            IOrganizationRepository repository,
            OrganizationManager manager,
            IDataAppService dataAppService)
        {
            _repository = repository;
            _manager = manager;
            _dataAppService = dataAppService;
        }

        public async Task<OrganizationDto> CreateAsync(OrganizationDto dto)
        {
            var organization = await _manager.Create(dto.ParentId, dto.No, dto.Name, dto.Type, dto.Phone, dto.Fax, dto.Descriptions);
            await _repository.InsertAsync(organization);

            return ObjectMapper.Map<Organization, OrganizationDto>(organization);
        }

        [HttpGet]
        public async Task<GetDataNameListDto> CreateAsync()
        {
            var dtos = await _dataAppService.GetListAsync(new string[] { "组织结构类别列表" });
            return dtos.FirstOrDefault();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _manager.DeleteAsync(id);
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

        [Route("api/core/organization/parentid/{parentId}")]
        public async Task<List<OrganizationDto>> GetListAsync(string parentId)
        {
            var organizations = await _repository.GetListAsync(QueryEnum.Parent, parentId);
            return ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(organizations);
        }

        public async Task<List<OrganizationDto>> GetListAsync()
        {
            var organizations = await _repository.GetListAsync();
            return ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(organizations);
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

                await _manager.UpdateAsync(organization, dto.Name, dto.Descriptions);
            }
        }
    }
}
