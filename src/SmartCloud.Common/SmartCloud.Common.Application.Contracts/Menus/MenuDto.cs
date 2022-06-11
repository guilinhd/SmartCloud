

using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Menus
{
    public class MenuDto : AuditedEntityDto<Guid>
    {
        public int Category { get; set; }

        public int No { get; set; }

        public string ParentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Href { get; set; }

        public string ImageName { get; set; }

        public string Parameter { get; set; }

        public TypeEnum Type { get; set; }
    }
}
