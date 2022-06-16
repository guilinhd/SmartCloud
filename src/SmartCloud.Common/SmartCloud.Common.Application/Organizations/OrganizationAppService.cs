using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.DataIndexs;
using SmartCloud.Common.Datas;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Organizations
{
    public class OrganizationAppService : ApplicationService, IOrganizationAppService
    {
        private readonly IOrganizationRepository _repository;
        private readonly OrganizationManager _manager;
        private readonly DataManager _dataManager;

        public OrganizationAppService(
            IOrganizationRepository repository,
            OrganizationManager manager,
            DataManager dataManager
        )
        {
            _repository = repository;
            _manager = manager;
            _dataManager = dataManager;
        }

        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="dto">实体</param>
        public async Task<OrganizationDto> CreateAsync(OrganizationDto dto)
        {
            var organization = await _manager.CreateAsync(
                dto.ParentId, 
                dto.No, 
                dto.Name, 
                dto.Type, 
                dto.Phone, 
                dto.Fax, 
                dto.Descriptions
            );

            return ObjectMapper.Map<Organization, OrganizationDto>(organization);
        }

        /// <summary>
        /// 新增初始化
        /// </summary>
        /// <returns>数据字典</returns>
        [HttpGet]
        public async Task<CreateOrganizationDto> CreateAsync()
        {
            CreateOrganizationDto dto = new();

            dto.Organizations = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(await _repository.GetListAsync());
            dto.Datas = await _dataManager.GetNameAsync("组织结构说明", "类型");

            return dto;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _manager.DeleteAsync(id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<OrganizationDto> GetAsync(Guid id)
        {
            var organization = await _repository.GetAsync(id);
            return ObjectMapper.Map<Organization, OrganizationDto>(organization);
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid id, OrganizationDto dto)
        {
            var organization = await _repository.GetAsync(id);

            await _manager.UpdateAsync(
                organization, 
                dto.No,
                dto.Name, 
                dto.Type,
                dto.Phone,
                dto.Fax,
                dto.Descriptions
            );
        }
    }
}
