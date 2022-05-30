using SmartCloud.Common.DataIndexs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace SmartCloud.Common.Domain.DataIndexs
{
    public class DataIndex : AuditedAggregateRoot<Guid>
    {
        public string Name { private set; get; }

        public string Description { set; get; }

        public string Reader { set; get; }

        public string Editor { set; get; }

        private DataIndex() { }

        internal DataIndex(
            Guid id,
            string name,
            List<Description> descriptions,
            string reader,
            string editor
            ) : base(id)
        {
            Name = name;
            Description = JsonSerializer.Serialize(
                descriptions, 
                new JsonSerializerOptions(){Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All)}
                );
            Reader = reader;
            Editor = editor;
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
