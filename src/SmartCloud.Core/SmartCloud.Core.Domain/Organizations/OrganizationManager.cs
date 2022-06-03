
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Volo.Abp;
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
        /// 创建类别实体
        /// </summary>
        /// <param name="name">类别名称</param>
        /// <returns>类别实体</returns>
        /// <exception cref="OrganizationAlreadyExistsException">类别名称重复</exception>
        public async Task<Organization> Create(
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
            var existingOrganization = await _repository.GetListAsync(QueryEnum.Name, name);
            if (existingOrganization.Count > 0)
            {
                throw new OrganizationAlreadyExistsException(name);
            }
            #endregion

            #region 上级组织结构的核算科目
            string parentAccounting = ""; int category = 1;
            if (parentId != "")
            {
                var parentOrganization = await _repository.GetAsync(new Guid(parentId));
                if (parentOrganization != null)
                { 
                    parentAccounting = parentOrganization.Accounting;
                    category = parentOrganization.Category + 1;
                }
            }
            #endregion

            //同级组织结构的核算科目
            string accounting = parentAccounting + await GetAccounting(parentId);

            //生成实体
            return new Organization(
                GuidGenerator.Create(), 
                parentId,
                category,
                no,
                name,
                type,
                phone,
                fax,
                accounting,
                descriptions
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Organization organization, 
            [NotNull] string newName
        )
        {
            Check.NotNull(organization, nameof(organization));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingOrganization = await _repository.GetListAsync(QueryEnum.Name, newName);
            if (existingOrganization.Count > 0 && existingOrganization.First().Id != organization.Id)
            {
                throw new OrganizationAlreadyExistsException(organization.Name);
            }

            organization.ChangeName(newName);
            await _repository.UpdateAsync(organization); 
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
            if (organization.ParentId == parentId)
{
                throw new OrganizationAlreadyExistsException(name);
            }

            var parentOrganization = await _repository.GetAsync(new Guid(parentId));
            if (organization != null)
            {
                organization.Category = category;
                organization.ParentId = parentId;
                organization.Accounting = parentOrganization.Accounting + await GetAccounting(parentId);

                await _repository.UpdateAsync(organization);
            }
        }

        /// <summary>
        /// 获取当前组织结构的核算编号
        /// </summary>
        /// <param name="parentId">上级组织结构</param>
        /// <returns>核算编号</returns>
        private async Task<string> GetAccounting(string parentId)
        {
            var organizations = await _repository.GetListAsync(QueryEnum.Parent, parentId);
            if (organizations.Count == 0)
            {
                return "0001";
            }
            else
            {
                var accounting = organizations.Max(d => d.Accounting);
                return (Convert.ToInt16(accounting.Right(4)) + 1).ToString("d:4");
            }
        }
    }
}
