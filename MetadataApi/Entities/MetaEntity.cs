using MetadataApi.Model.Entities.Abstractions;
using System.Collections.Generic;

namespace MetadataApi.Model.Entities
{
    public class MetaEntity : MetaEntityBase
    {
        public virtual IEnumerable<MetaEntityField> Fields { get; set; }

    }
}
