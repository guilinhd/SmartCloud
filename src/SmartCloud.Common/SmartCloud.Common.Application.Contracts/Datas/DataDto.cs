
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Datas
{
    public class DataDto : AuditedEntityDto<Guid>
    {
        [Required]
        public Guid CategoryId { set; get; }

        [Required]
        public string Category { set; get; }

        public int No { set; get; } = 999;

        public string Name { set; get; } = "";

        public string Remark1 { set; get; } = "";

        public string Remark2 { set; get; } = "";

        public string Remark3 { set; get; } = "";

        public string Remark4 { set; get; } = "";

        public string Remark5 { set; get; } = "";

        public string Remark6 { set; get; } = "";

        public string Remark7 { set; get; } = "";

        public string Remark8 { set; get; } = "";

        public string Remark9 { set; get; } = "";

        public string Remark10 { set; get; } = "";

        public string Remark11 { set; get; } = "";

        public string Remark12 { set; get; } = "";

        public string Remark13 { set; get; } = "";

        public string Remark14 { set; get; } = "";

        public string Remark15 { set; get; } = "";
    }
}
