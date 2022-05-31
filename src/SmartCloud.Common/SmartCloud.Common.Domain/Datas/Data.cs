using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SmartCloud.Common.Datas
{
    public class Data : AuditedAggregateRoot<Guid>
    {
        [Required]
        public Guid CategoryId { set; get; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string Category { set; get; }

        public int No { set; get; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string Name { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark1 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark2 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark3 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark4 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark5 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark6 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark7 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark8 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark9 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark10 { set; get; }

        [Column(TypeName = "varchar(600)")]
        public string Remark11 { set; get; }

        [Column(TypeName = "text")]
        public string Remark12 { set; get; }

        [Column(TypeName = "text")]
        public string Remark13 { set; get; }

        [Column(TypeName = "text")]
        public string Remark14 { set; get; }

        [Column(TypeName = "text")]
        public string Remark15 { set; get; }

    }
}
