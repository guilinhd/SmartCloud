

namespace SmartCloud.Common.Users
{
    public class CreateSaveUserDto : UserDto
    {
        /// <summary>
        /// 新增加的角色  string-角色id  guid-roleuserid
        /// </summary>
        public string[] Roles { get; set; }
    }
}
