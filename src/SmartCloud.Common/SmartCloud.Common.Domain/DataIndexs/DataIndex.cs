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
            string name,
            string description
            ) : base(id)
        {
            Name = name;
            Reader = "";
            Editor = "";
            Description = description;
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
