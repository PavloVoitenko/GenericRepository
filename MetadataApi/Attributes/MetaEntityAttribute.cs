using MetadataApi.Attributes.Abstractions;

using System;

namespace MetadataApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MetaEntityAttribute : MetaAttribute
    {
        public string DisplayName { get; private set; }

        public MetaEntityAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
