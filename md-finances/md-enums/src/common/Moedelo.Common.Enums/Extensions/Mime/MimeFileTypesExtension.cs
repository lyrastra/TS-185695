using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.Mime;
using Moedelo.Common.Enums.Enums.MimeTypes;

namespace Moedelo.Common.Enums.Extensions.Mime
{
    public static class MimeFileTypesExtension
    {
        private static readonly IReadOnlyDictionary<MimeFileTypes, string> Map =
            Enum.GetValues(typeof(MimeFileTypes))
                .Cast<MimeFileTypes>()
                .ToDictionary(value => value,
                    value => value.GetEnumAttribute<MimeTypeAttribute, MimeFileTypes>().Value);
        
        public static string ToMimeString(this MimeFileTypes type)
        {
            return Map.TryGetValue(type, out var text) ? text : null;
        }
    }
}