using System;
using Moedelo.Common.Enums.Attributes;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.Common.Enums.Extensions
{
    public static class DocumentFormatExtensions
    {
        /// <summary>
        /// Возвращает расширение файла, соответствующее указанному формату
        /// </summary>
        public static string GetFileExtension(this DocumentFormat value)
        {
            var attribute = value.GetAttribute<FileExtensionAttribute>();
            if (attribute == null)
            {
                throw new ArgumentException($"Unable find FileExtensionAttribute for {nameof(DocumentFormat)}.{value}. Did you forget to set it up?");
            }
            return attribute.Extension;
        }

        /// <summary>
        /// Возвращает расширение файла с точкой префиксом (например, ".pdf"), соответствующее указанному формату
        /// </summary>
        public static string GetFileDotExtension(this DocumentFormat value)
        {
            return $".{value.GetFileExtension()}";
        }
    }
}