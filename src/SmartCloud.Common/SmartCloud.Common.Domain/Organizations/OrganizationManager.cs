using Microsoft.Extensions.Options;
using System.Text.Json;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using System.Diagnostics.CodeAnalysis;

namespace SmartCloud.Common.Organizations
{
    public class OrganizationManager : DomainService
    {
        private readonly IOrganizationRepository _repository;
        private readonly IOptions<JsonSerializerOptions> _options;

        public OrganizationManager(
            IOrganizationRepository repository,
            IOptions<JsonSerializerOptions> options)
        {
            _repository = repository;
            _options = options;
        }

        /// <summary>
        /// 调整组织结构的所属上级
        /// </summary>
        /// <param name="id">组织结构id</param>
        /// <param name="category">在组织结构所在的层级</param>
        /// <param name="parentId">上级组织结构id</param>
        /// <returns></returns>
        public async Task AdjustAsync(Guid id, int category, string parentId)
        {
            var organization = await _repository.GetAsync(id);
            if (organization.ParentId == parentId)
            {
                throw new OrganizationAlreadyExistsException(organization.Name);
            }

            var parentOrganization = await _repository.GetAsync(new Guid(parentId));
            if (organization != null)
            {
                organization.Category = category;
                organization.ParentId = parentId;
                organization.Accounting = parentOrganization.Accounting + await GetAccountingAsync(parentId);

                await _repository.UpdateAsync(organization);
            }
        }

        /// <summary>
        /// 创建组织结构实体
        /// </summary>
        /// <param name="parentId">上级组织结构Id</param>
        /// <param name="no">序号</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="phone">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="descriptions">描述信息</param>
        /// <returns>组织结构实体</returns>
        /// <exception cref="OrganizationAlreadyExistsException">组织结构名称重复</exception>
        public async Task<Organization> CreateAsync(
            string parentId,
            int no,
            [NotNull] string name,
            string type,
            string phone,
            string fax,
            List<Description> descriptions
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            #region 名称是否重复
            var existingOrganizations = await _repository.GetListAsync(QueryEnum.Name, name);
            if (existingOrganizations.Count > 0)
            {
                throw new OrganizationAlreadyExistsException(name);
            }
            #endregion

            #region 上级组织结构的核算科目
            string parentAccounting = ""; int category = 1;
            if (parentId != "")
            {
                var parentOrganizations = await _repository.GetListAsync(QueryEnum.Id, parentId);
                if (parentOrganizations.Count > 0)
                {
                    parentAccounting = parentOrganizations.First().Accounting;
                    category = parentOrganizations.First().Category + 1;
                }
            }
            #endregion

            #region 生成实体
            //同级组织结构的核算科目
            string accounting = parentAccounting + await GetAccountingAsync(parentId);

            var organization = new Organization(
                GuidGenerator.Create(),
                parentId,
                category,
                no,
                name,
                type,
                phone,
                fax,
                accounting,
                JsonSerializer.Serialize(descriptions, _options.Value)
            );
            #endregion

            await _repository.InsertAsync(organization);
            return organization;
        }

        /// <summary>
        /// 删除组织结构实体
        /// </summary>
        /// <param name="id">组织结构id</param>
        /// <returns></returns>
        /// <exception cref="OrganizationHasChildrenException">组织结构删除失败</exception>
        public async Task DeleteAsync(Guid id)
        {
            var organization = await _repository.GetAsync(id);
            if (organization != null)
            {
                var organizationChildren = await _repository.GetListAsync(QueryEnum.Parent, organization.Id.ToString());
                if (organizationChildren.Count > 0)
                {
                    throw new OrganizationHasChildrenException(organization.Name);
                }

                await _repository.DeleteAsync(organization);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns>列表</returns>
        public async Task<Dictionary<Guid, string>> GetListAsync()
        {
            Dictionary<Guid, string> names = new ();

            var organizations = await _repository.GetListAsync();
            organizations.ForEach(organization => {
                names.Add(organization.Id, organization.Name);
            });

            return names;
        }

        /// <summary>
        /// 修改组织结构实体
        /// </summary>
        /// <param name="organization">组织结构实体</param>
        /// <param name="newName">新名称</param>
        /// <param name="descriptions">描述信息</param>
        /// <exception cref="OrganizationAlreadyExistsException">组织结构名称重复</exception>
        public async Task UpdateAsync(
            [NotNull] Organization organization,
            int no,
            [NotNull] string name,
            string type,
            string phone,
            string fax,
            List<Description> descriptions)
        {
            Check.NotNull(organization, nameof(organization));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingOrganizations = await _repository.GetListAsync(QueryEnum.Name, name);
            if (existingOrganizations.Count > 0 && existingOrganizations.First().Id != organization.Id)
            {
                throw new OrganizationAlreadyExistsException(name);
            }
            
            organization.No = no;
            organization.ChangeName(name);
            organization.Type = type;
            organization.Phone = phone;
            organization.Fax = fax;
            organization.Description = JsonSerializer.Serialize(descriptions, _options.Value);
            
            await _repository.UpdateAsync(organization);
        }

        /// <summary>
        /// 获取当前组织结构的核算编号
        /// </summary>
        /// <param name="parentId">上级组织结构</param>
        /// <returns>核算编号</returns>
        private async Task<string> GetAccountingAsync(string parentId)
        {
            var organizations = await _repository.GetListAsync(QueryEnum.Parent, parentId);
            if (organizations.Count == 0)
            {
                return "0001";
            }
            else
            {
                var accounting = organizations.Max(d => d.Accounting);
                return (Convert.ToInt16(accounting.Right(4)) + 1).ToString("D4");
            }
        }
    }
}
