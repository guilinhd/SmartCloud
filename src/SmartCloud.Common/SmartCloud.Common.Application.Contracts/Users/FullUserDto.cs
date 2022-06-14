using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Users
{
    public class FullUserDto : UserDto
    {
        public string OrganizationName { get; set; }

        public string OrganizationAccounting { get; set; }
    }
}
