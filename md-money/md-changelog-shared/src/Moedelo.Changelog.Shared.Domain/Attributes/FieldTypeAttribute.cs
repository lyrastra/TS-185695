using System;

namespace Moedelo.Changelog.Shared.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal abstract class FieldTypeAttribute : Attribute
    {
        public string FieldType { get; }

        protected FieldTypeAttribute(string fieldType)
        {
            FieldType = fieldType;
        }
    }
}
