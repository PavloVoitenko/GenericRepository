using MetadataApi.Model.Entities.Abstractions;

namespace MetadataApi.Model.Entities
{
    public class MetaEntityField : MetaEntityBase
    {
        public DataType Type { get; set; }
        public EditableType Editable { get; set; }
        public bool Visible { get; set; }
        public int MetaEntityId { get; set; }

        public virtual MetaEntity Entity { get; set; }
    }

    public enum DataType
    {
        String,
        Time,
        Date,
        Number
    }
}
