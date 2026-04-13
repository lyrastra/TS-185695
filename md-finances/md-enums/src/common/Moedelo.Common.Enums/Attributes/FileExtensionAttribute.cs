using System;

namespace Moedelo.Common.Enums.Attributes
{
    public class FileExtensionAttribute : Attribute
    {
        public string Extension { get; }

        public FileExtensionAttribute(string ext)
        {
            Extension = ext?.ToLower();
        }
    }
}