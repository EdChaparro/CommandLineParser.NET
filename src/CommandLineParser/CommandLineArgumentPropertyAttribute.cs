using System;

namespace IntrepidProducts.CommandLineParser
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CommandLineArgumentPropertyAttribute : Attribute
    {
        public CommandLineArgumentPropertyAttribute
            (String key, object defaultValue, string description, bool isUndocumented = false)
        {
            Key = key;
            DefaultValue = defaultValue;
            Description = description;
            IsUndocumented = isUndocumented;
        }

        public string Key { get; }
        public string Description { get; }
        public object DefaultValue { get; }
        public bool IsUndocumented { get; }
    }
}