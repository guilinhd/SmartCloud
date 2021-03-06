
using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Menus;
using SmartCloud.Common.Organizations;
using SmartCloud.Common.RoleMenus;
using SmartCloud.Common.Roles;
using SmartCloud.Common.RoleUsers;
using System.Security.Cryptography;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager _manager;
        private readonly DataManager _dataManager;
        private readonly IOrganizationAppService _organizationAppService;
        private readonly IMenuAppService _menuAppService;
        private readonly RoleManager _roleManager;
        private readonly RoleUserManager _roleUserManager;
        private readonly RoleMenuManager _roleMenuManager;

        public UserAppService(
            IUserRepository repository,
            UserManager manager,
            DataManager dataManager,
            IOrganizationAppService organizationAppService,
            IMenuAppService menuAppService,
            RoleManager roleManager,
            RoleUserManager roleUserManager,
            RoleMenuManager roleMenuManager
        )
        {
            _repository = repository;
            _manager = manager;
            _dataManager = dataManager;
            _organizationAppService = organizationAppService;
            _menuAppService = menuAppService;
            _roleManager = roleManager;
            _roleUserManager = roleUserManager;
            _roleMenuManager = roleMenuManager;
        }

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public async Task<SaveUserDto> CreateAsync(CreateSaveUserDto dto)
        {
            #region 新增存盘
            string pwdSalt = CreateSalt(6);
            var user = await _manager.CreateAsync(
                dto.OrganizationId,
                dto.No,
                dto.Name,
                GetPwd(pwdSalt),
                pwdSalt,
                dto.Gender,
                dto.Phone,
                dto.Mobile,
                dto.Fax,
                dto.Post,
                dto.Descriptions
            );
            #endregion

            var saveUserDto = ObjectMapper.Map<User, SaveUserDto>(user);

            #region 角色人员新增存盘
            var roleUses = new List<RoleUserDto>();
            foreach (var roleId in dto.Roles)
            {
                var result = await _roleUserManager.CreateAsync(roleId, new string[] { user.Id.ToString() });
                roleUses.Add(ObjectMapper.Map<RoleUser, RoleUserDto>(result.First()));
            }
            saveUserDto.RoleUses = roleUses;
            #endregion

            saveUserDto.Menus = await GetRoleMenusAsync(dto.Roles);

            return saveUserDto;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/common/user/create")]
        public async Task<CreateUserDto> CreateAsync()
        {
            CreateUserDto dto = new();

            //组织结构列表
            dto.Organization = await _organizationAppService.GetNodeAsync();

            //人员列表
            dto.Users = ObjectMapper.Map<List<User>, List<PartUserDto>>(await _manager.GetListAsync());

            //角色列表
            var roles = await _roleManager.GetListAsync();
            dto.Roles = new();
            roles.ForEach(role => {
                dto.Roles.Add(role.Id, role.Name);
            });

            //菜单列表
            dto.Menu = await _menuAppService.GetNodeAsync();

            var datas =  await _dataManager.GetNameListAsync(new string[] { "职务列表" });
            dto.Datas = datas.First().Value;

            return dto;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);

            //删除角色用户
            var roleUsers = await _roleUserManager.GetListAsync(RoleUsers.QueryEnum.UserId, user.Id.ToString());
            await _roleUserManager.DeleteAsync(roleUsers.Select(d => d.Id.ToString()).ToArray());

            //删除
            await _manager.DeleteAsync(user);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SaveUserDto> GetAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);

            var dto = ObjectMapper.Map<User, SaveUserDto>(user);

            var roleUsers = await _roleUserManager.GetListAsync(RoleUsers.QueryEnum.UserId, user.Id.ToString());
            dto.RoleUses = ObjectMapper.Map<List<RoleUser>, List<RoleUserDto>>(roleUsers);

            dto.Menus = await GetRoleMenusAsync(roleUsers.Select(d => d.RoleId).ToArray());

            return dto;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>实体</returns>
        [RemoteService(false)]
        public async Task<FullUserDto> GetAsync(string name, string pwd)
        {
            var user = await _manager.GetAsync(name);
            string inputPwd = GetPwd(user.PwdSalt, pwd);

            if (user.Pwd != inputPwd)
            {
                return null;
            }

            return ObjectMapper.Map<User, FullUserDto>(user);
        }

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/common/user/pwd/reset")]
        public async Task PwdResetAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);

            //初始密码
            string pwd = GetPwd(user.PwdSalt);
            user.Pwd = pwd;
            await _manager.ChangePwdAsync(user, pwd, pwd); 
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/common/user/pwd/change")]
        public async Task PwdChangeAsync(ChangeUserPwdDto dto)
        {
            var user = await _repository.GetAsync(dto.Id);

            string oldPwd = GetPwd(user.PwdSalt, dto.Password);
            string newPwd = GetPwd(user.PwdSalt, dto.NewPassword);

            await _manager.ChangePwdAsync(user, oldPwd, newPwd);
        }

        /// <summary>
        /// 更改账户停用状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/common/user/status")]
        public async Task UpdateAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);
            await _manager.UpdateAsync(user);
        }

        /// <summary>
        /// 修改存盘
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public async Task<SaveUserDto> UpdateAsync(Guid id, UpdateSaveUserDto dto)
        {
            var saveUserDto = new SaveUserDto();

            #region 人员修改存盘
            var user = await _repository.GetAsync(id);
            //修改存盘
            await _manager.UpdateAsync(
                user,
                dto.No,
                dto.Name,
                dto.Gender,
                dto.Phone,
                dto.Mobile,
                dto.Fax,
                dto.Post,
                dto.Descriptions
            );
            saveUserDto.Id = id;
            #endregion

            #region 角色人员新增存盘
            var roleUses = new List<RoleUserDto>();
            foreach (var roleId in dto.Roles)
            {
                var result = await _roleUserManager.CreateAsync(roleId, new string[] { id.ToString() });
                roleUses.Add(ObjectMapper.Map<RoleUser, RoleUserDto>(result.First()));
            }
            saveUserDto.RoleUses = roleUses;
            #endregion

            #region 角色人员删除
            await _roleUserManager.DeleteAsync(dto.RoleUsers);
            #endregion

            saveUserDto.Menus = await GetRoleMenusAsync(dto.Roles);

            return saveUserDto;
        }

        

        /// <summary>
        /// 生成密码
        /// </summary>
        /// <param name="pwd">密码明文</param>
        /// <param name="pwdSalt">密码盐</param>
        /// <returns></returns>
        private string GetPwd(string pwdSalt, string pwd = "1234")
        {
            return Md5For32(string.Concat(pwdSalt.AsSpan(0, 2), Md5For32(pwd), pwdSalt.AsSpan(2)));
        }

        /// <summary>
        /// 获取32位md5加密 通过创建哈希字符串适用于任何 MD5 哈希函数 （在任何平台） 上创建 32 个字符的十六进制格式哈希字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns>32位md5加密字符串</returns>
        private string Md5For32(string source)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder sBuilder = new();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            string hash = sBuilder.ToString();
            return hash.ToUpper();
        }

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns></returns>
        private string CreateSalt(int length)
        {
            string result = "";

            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            int n = Pattern.Length;
            Random random = new(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }

            return result;
        }

        /// <summary>
        /// 按角色id查询菜单id
        /// </summary>
        /// <param name="roleIds">角色id</param>
        /// <returns></returns>
        private async Task<string[]> GetRoleMenusAsync(string[] roleIds)
        {
            var roleMenus = new List<RoleMenu>();
            foreach (var roleId in roleIds)
            {
                roleMenus.AddRange(await _roleMenuManager.GetListAsync(RoleMenus.QueryEnum.RoleId, roleId));
            }

            return roleMenus.Select(d => d.MenuId).ToArray();
        }
    }
}
