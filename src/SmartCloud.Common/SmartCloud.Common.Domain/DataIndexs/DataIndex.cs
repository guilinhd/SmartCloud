using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Unicode;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SmartCloud.Common.DataIndexs
{
    public class DataIndex : AggregateRoot<Guid>
    {
        public string Name { get; private set;  }
        
        public string Description { get; set;  } = "";
        
        public string Reader { get; set;  } = "";

        public string Editor { get; set;  } = "";

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
                new JsonSerializerOptions() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All) });
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
