using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataApi.Model.Entities.Abstractions
{
    public class MetaEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
