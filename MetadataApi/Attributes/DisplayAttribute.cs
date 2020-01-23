using MetadataApi.Attributes.Abstractions;
using System;

namespace MetadataApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class DisplayAttribute : MetaAttribute
    {
        public string DisplayName { get; private set; }

        public DisplayAttribute(string displayAs)
        {
            DisplayName = displayAs;
        }
    }
}
