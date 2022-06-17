
namespace SmartCloud.Common.Users
{
    public class CreateUserDto
    {
        public INodeDto Organization { get; set; }

        public List<PartUserDto> Users { get; set; } = new();

        public INodeDto Menu { get; set; }

        public Dictionary<Guid, string> Roles { get; set; }

        public ICollection<string> Datas { get; set; }
    }
}
