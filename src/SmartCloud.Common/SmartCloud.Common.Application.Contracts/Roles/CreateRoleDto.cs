using SmartCloud.Common.Users;


namespace SmartCloud.Common.Roles
{
    public class CreateRoleDto
    {
        public List<RoleDto> Roles { get; set; }

        public INodeDto Organization { get; set; }

        public List<PartUserDto> Users { get; set; }

        public INodeDto Menu { get; set; }

        public ICollection<string> Datas { get; set; }
    }
}
