
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCloud.Common.Datas;
using SmartCloud.Common.Organizations;
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
        private readonly OrganizationManager _organizationManager;
        private readonly DataManager _dataManager;
        private readonly IDataAppService _dataAppService;

        public UserAppService(
            IUserRepository repository,
            UserManager manager,
            OrganizationManager organizationManager,
            DataManager dataManager,
            IDataAppService dataAppService
        )
        {
            _repository = repository;
            _manager = manager;
            _organizationManager = organizationManager;
            _dataManager = dataManager;
            _dataAppService = dataAppService;
        }

        /// <summary>
        /// 新增存盘
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public async Task<CreateUpdateUserDto> CreateAsync(CreateUpdateUserDto dto)
        {
            //新增存盘
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

            return ObjectMapper.Map<User, CreateUpdateUserDto>(user);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/common/user/create")]
        public async Task<Dictionary<string, Dictionary<Guid, string>>> CreateAsync()
        {
            Dictionary<string, Dictionary<Guid, string>> dto = new();

            var organizations = await _organizationManager.GetListAsync();
            dto.Add("组织结构列表", organizations);

            var users = await _manager.GetListAsync();
            dto.Add("人员列表", users);

            var datas = await _dataManager.GetNameListAsync(new string[] { "职务列表" });
            Dictionary<Guid, string> names = new();
            foreach (var item in datas.First().Value)
            {
                names.Add(Guid.NewGuid(), item);
            }
            dto.Add("职务", names);

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
            //删除
            await _manager.DeleteAsync(user);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CreateUpdateUserDto> GetAsync(Guid id)
        {
            var user = await _repository.GetAsync(id);
            return ObjectMapper.Map<User, CreateUpdateUserDto>(user);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>实体</returns>
        [RemoteService(false)]
        [AllowAnonymous]
        public async Task<UserDto> GetAsync(string name, string pwd)
        {
            var user = await _manager.GetAsync(name);
            string inputPwd = GetPwd(user.PwdSalt, pwd);

            if (user.Pwd != inputPwd)
            {
                return null;
            }

            return ObjectMapper.Map<User, UserDto>(user);
        }

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

        [HttpPut]
        [Route("api/common/user/pwd/change")]
        public async Task PwdChangeAsync(ChangeUserPwdDto dto)
        {
            var user = await _repository.GetAsync(dto.id);

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
        public async Task UpdateAsync(Guid id, CreateUpdateUserDto dto)
        {
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

        
    }
}
