using System;

namespace Moedelo.Common.Enums.Enums.Mime
{
    public class MimeTypeAttribute : Attribute
    {
        public MimeTypeAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}