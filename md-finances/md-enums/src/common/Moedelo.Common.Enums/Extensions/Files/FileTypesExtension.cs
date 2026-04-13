using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Attributes;
using Moedelo.Common.Enums.Enums.Mime;
using Moedelo.Common.Enums.Enums.MimeTypes;

namespace Moedelo.Common.Enums.Extensions.Mime;

public static class FileTypesExtension
{
    private static readonly ConcurrentDictionary<FileTypes, MimeFileTypes> DicMime =
        new ConcurrentDictionary<FileTypes, MimeFileTypes>
        {
            [FileTypes.Bin] = MimeFileTypes.OctetStream,
            [FileTypes.Bmp] = MimeFileTypes.Bmp,
            [FileTypes.Csv] = MimeFileTypes.Csv,
            [FileTypes.Doc] = MimeFileTypes.Word,
            [FileTypes.Dib] = MimeFileTypes.Dib,
            [FileTypes.Docx] = MimeFileTypes.Word,
            [FileTypes.Xls] = MimeFileTypes.Excel,
            [FileTypes.Xlsx] = MimeFileTypes.Excel,
            [FileTypes.Gif] = MimeFileTypes.Gif,
            [FileTypes.Htm] = MimeFileTypes.Html,
            [FileTypes.Html] = MimeFileTypes.Html,
            [FileTypes.Jfif] = MimeFileTypes.Jpeg,
            [FileTypes.Jpg] = MimeFileTypes.Jpeg,
            [FileTypes.Jpe] = MimeFileTypes.Jpeg,
            [FileTypes.Jpeg] = MimeFileTypes.Jpeg,
            [FileTypes.Pdf] = MimeFileTypes.Pdf,
            [FileTypes.Png] = MimeFileTypes.Png,
            [FileTypes.Rar] = MimeFileTypes.Rar,
            [FileTypes.Rtf] = MimeFileTypes.Rtf,
            [FileTypes.Tif] = MimeFileTypes.Tiff,
            [FileTypes.Tiff] = MimeFileTypes.Tiff,
            [FileTypes.Txt] = MimeFileTypes.Txt,
            [FileTypes.Xml] = MimeFileTypes.Xml,
            [FileTypes.Zip] = MimeFileTypes.Zip,
            [FileTypes.Zip7] = MimeFileTypes.Zip7,
            [FileTypes.Gzip] = MimeFileTypes.Gzip,
            [FileTypes.Bzip2] = MimeFileTypes.Bzip2,
            [FileTypes.Heic] = MimeFileTypes.Heic,
        };

    private static readonly IReadOnlyDictionary<string, FileTypes> DicFileExtensions;

    static FileTypesExtension()
    {
        var names = Enum.GetNames(typeof(FileTypes));
        var type = typeof(FileTypes);

        var map = new Dictionary<string, FileTypes>();
        
        foreach (var name in names)
        {
            var prop = type.GetMember(name);
            var attr = prop[0].GetCustomAttributes(false).OfType<FileExtensionAttribute>().FirstOrDefault();
            if (attr == null)
            {
                continue;
            }

            var fileType = (FileTypes)Enum.Parse(type, name);
            map[attr.Extension] = fileType;
            map["." + attr.Extension] = fileType;
        }

        DicFileExtensions = map;
    }

    public static MimeFileTypes? ToMime(this FileTypes type)
    {
        if (DicMime.TryGetValue(type, out var result))
        {
            return result;
        }

        return null;
    }

    public static FileTypes? FromString(string ext)
    {
        if (string.IsNullOrWhiteSpace(ext))
        {
            return null;
        }

        if (DicFileExtensions.TryGetValue(ext.ToLower(), out var type))
        {
            return type;
        }

        return null;
    }
}
