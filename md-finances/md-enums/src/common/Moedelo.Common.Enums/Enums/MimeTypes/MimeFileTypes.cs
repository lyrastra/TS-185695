using Moedelo.Common.Enums.Enums.Mime;

namespace Moedelo.Common.Enums.Enums.MimeTypes
{
    public enum MimeFileTypes
    {
        [MimeType("application/octet-stream")] OctetStream,
        [MimeType("application/json")] Json,
        [MimeType("application/jose")] Jose,
        [MimeType("application/ms-word")] Word,
        [MimeType("application/ms-excel")] Excel,
        [MimeType("application/x-rar-compressed")] Rar,
        [MimeType("application/zip")] Zip,
        [MimeType("application/x-7z-compressed")] Zip7,
        [MimeType("application/x-gzip")] Gzip,
        [MimeType("application/x-bzip2")] Bzip2,
        [MimeType("text/csv")] Csv,
        [MimeType("text/html")] Html,
        [MimeType("image/png")] Png,
        [MimeType("image/bmp")] Bmp,
        [MimeType("image/tiff")] Tiff,
        [MimeType("image/jpeg")] Jpeg,
        [MimeType("image/gif")] Gif,
        [MimeType("application/pdf")] Pdf,
        [MimeType("application/rtf")] Rtf,
        [MimeType("text/plain")] Txt,
        [MimeType("text/xml")] Xml,
        [MimeType("image/heic")] Heic,
        [MimeType("image/dib")] Dib,
    }
}