using SmartCloud.Common.DataIndexs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace SmartCloud.Common.DataIndexs
{
    public class DataIndex : AuditedAggregateRoot<Guid>
    {
        [Required]
        [Column(TypeName = "varchar(60)")]

        public string Name { private set; get; }

        [Column(TypeName = "text")]
        public string Description { set; get; } = "";

        [Column(TypeName = "text")]
        public string Reader { set; get; } = "";

        [Column(TypeName = "text")]
        public string Editor { set; get; } = "";

        private DataIndex() { }

        internal DataIndex(
            Guid id,
            string name
            ) : base(id)
        {

            #region 初始化类型描述
            List<Description> descriptions = new List<Description>();
            descriptions.Add(new Description()
            {
                No = 1,
                Name = "Name",
                Title = "名称",
                Content = ""
            });
            for (int i = 1; i <= 15; i++)
            {
                descriptions.Add(new Description()
                {
                    No = i + 1,
                    Width = 120,
                    Name = "Remark" + i.ToString(),
                    Title = "备注" + i.ToString(),
                    Content = ""
                });
            }
            #endregion

            Name = name;
            Reader = "";
            Editor = "";
            Description = JsonSerializer.Serialize(
                descriptions,
                new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All) }); ;
        }

        internal DataIndex ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name)
            );
        }
    }
}
