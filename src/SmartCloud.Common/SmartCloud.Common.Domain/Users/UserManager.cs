using Microsoft.Extensions.Options;
using SmartCloud.Common.Organizations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;


namespace SmartCloud.Common.Users
{
    public class UserManager : DomainService
    {
        private readonly IUserRepository _repository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IOptions<JsonSerializerOptions> _options;

        public UserManager(
            IUserRepository repository,
            IOrganizationRepository organizationRepository,
            IOptions<JsonSerializerOptions> options
        )
        {
            _repository = repository;
            _organizationRepository = organizationRepository;
            _options = options;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="organizationId">组织结构Id</param>
        /// <param name="no">序号</param>
        /// <param name="name">用户名</param>
        /// <param name="gender">性别</param>
        /// <param name="phone">电话</param>
        /// <param name="mobile">手机</param>
        /// <param name="fax">传真</param>
        /// <param name="post">职务</param>
        /// <param name="descriptions">描述</param>
        /// <returns></returns>
        /// <exception cref="UserAlreadyExistsException">用户名重复</exception>
        public async Task<User> CreateAsync(
            [NotNull] string organizationId,
            int no,
            [NotNull] string name,
            string pwd,
            string pwdSalt,
            string gender,
            string phone,
            string mobile,
            string fax,
            string post,
            List<Description> descriptions
         )
        {
            Check.NotNullOrWhiteSpace(organizationId, nameof(organizationId));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            #region 名称是否重复
            var existingUsers = await _repository.GetListAsync(QueryEnum.Name, name);
            if (existingUsers.Count > 0)
            {
                throw new UserAlreadyExistsException(name);
            }
            #endregion

            #region 生成实体
            var user = new User(
                GuidGenerator.Create(),
                organizationId,
                no,
                name,
                pwd,
                pwdSalt,
                gender,
                phone,
                mobile,
                fax,
                post,
                JsonSerializer.Serialize(descriptions, _options.Value)
            );
            #endregion

            await _repository.InsertAsync(user);
            return user;
        }

        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="user">实体</param>
        /// <returns></returns>
        public async Task DeleteAsync([NotNull] User user)
        {
            Check.NotNull(user, nameof(user));
            await _repository.DeleteAsync(user);
        }

        

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user">实体</param>
        /// <param name="pwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        /// <exception cref="UserPwdInvalidException">用户名或者密码不正确</exception>
        public async Task ChangePwdAsync(
            [NotNull] User user,
            [NotNull] string oldPwd,
            [NotNull] string newPwd
        )
        {
            Check.NotNull(user, nameof(user));
            Check.NotNullOrWhiteSpace(oldPwd, nameof(oldPwd));
            Check.NotNullOrWhiteSpace(newPwd, nameof(newPwd));

            if (oldPwd != user.Pwd)
            {
                throw new UserPwdInvalidException(user.Name);
            }

            user.Pwd = newPwd;
            await _repository.UpdateAsync(user);
        }

        /// <summary>
        /// 按用户名查询
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        public async Task<User> GetAsync(string name)
        {
            
            var queryable = await _repository.GetQueryableAsync();

            var query = from user in queryable
                        join organization in await _organizationRepository.GetQueryableAsync() on user.OrganizationId equals organization.Id.ToString()
                        where user.Name == name
                        select new {user, organization};

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(User), name);
            }

            var resutl = queryResult.user;
            resutl.Organization = queryResult.organization;

            return resutl;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetListAsync()
        {
            return await _repository.GetListAsync(); 
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="user">实体</param>
        /// <returns></returns>
        public async Task UpdateAsync([NotNull] User user)
        {
            Check.NotNull(user, nameof(user));
            user.Disable = user.Disable == 0 ? 1 : 0;

            await _repository.UpdateAsync(user);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user">实体</param>
        /// <param name="no">序号</param>
        /// <param name="name">用户名</param>
        /// <param name="gender">性别</param>
        /// <param name="phone">电话</param>
        /// <param name="mobile">手机</param>
        /// <param name="fax">传真</param>
        /// <param name="post">职务</param>
        /// <param name="descriptions">描述</param>
        /// <returns></returns>
        /// <exception cref="UserAlreadyExistsException">用户名重复</exception>
        public async Task UpdateAsync(
            [NotNull] User user,
            int no,
            [NotNull] string name,
            string gender,
            string phone,
            string mobile,
            string fax,
            string post,
            List<Description> descriptions
        )
        {
            Check.NotNull(user, nameof(user));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            #region 名称是否重复
            var existingUsers = await _repository.GetListAsync(QueryEnum.Name, name);
            if (existingUsers.Count > 0 && existingUsers.First().Id != user.Id)
            {
                throw new UserAlreadyExistsException(name);
            }
            #endregion

            user.No = no;
            user.ChangeName(name);
            user.Gender = gender;
            user.Phone = phone;
            user.Mobile = mobile;
            user.Fax = fax;
            user.Post = post;
            user.Description = JsonSerializer.Serialize(descriptions, _options.Value);

            await _repository.UpdateAsync(user);
        }
    }
}
