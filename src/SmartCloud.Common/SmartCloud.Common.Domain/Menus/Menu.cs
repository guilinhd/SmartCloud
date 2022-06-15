using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.Menus
{
    public class Menu : AggregateRoot<Guid>
    {
        public int Category { get; set; }

        public int No { get; set; }

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string Href { get; set; }

        public string ApiName { get; set; }

        public MethodEnum Method { get; set; }

        public string ImageName { get; set; }

        public string Parameter { get; set; }

        public TypeEnum Type { get; set; }
    }
}
