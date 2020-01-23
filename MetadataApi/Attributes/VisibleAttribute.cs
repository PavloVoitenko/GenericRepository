using MetadataApi.Attributes.Abstractions;
using System;

namespace MetadataApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class VisibleAttribute : MetaAttribute
    {
        public bool Visible { get; private set; }

        public VisibleAttribute(bool visible = true)
        {
            Visible = visible;
        }
    }
}
