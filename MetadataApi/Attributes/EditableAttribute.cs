using MetadataApi.Attributes.Abstractions;
using System;

namespace MetadataApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class EditableAttribute : MetaAttribute
    {
        public EditableType Editable { get; private set; }

        public EditableAttribute(EditableType editable)
        {
            Editable = editable;
        }
    }

    public enum EditableType
    {
        Readonly,
        OnCreate,
        Editable
    }
}
