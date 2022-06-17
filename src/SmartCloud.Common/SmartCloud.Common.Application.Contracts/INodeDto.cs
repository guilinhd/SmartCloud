

namespace SmartCloud.Common
{
    public interface INodeDto
    {
        public Guid Id { get; set; }

        public string ParentId { get; set; }

        public int Category { get; set; }

        public int No { get; set; }

        public string Name { get; set; }

        public List<INodeDto> Nodes { get; set; }
    }
}
