using Moedelo.Common.Enums.Attributes;

namespace Moedelo.Common.Enums.Enums.Mime
{
    public enum FileTypes
    {
        [FileExtension("tif")] Tif,
        [FileExtension("tiff")] Tiff,
        [FileExtension("png")] Png,
        [FileExtension("bmp")] Bmp,
        [FileExtension("gif")] Gif,
        [FileExtension("jpg")] Jpg,
        [FileExtension("jpeg")] Jpeg,
        [FileExtension("csv")] Csv,
        [FileExtension("doc")] Doc,
        [FileExtension("docx")] Docx,
        [FileExtension("xls")] Xls,
        [FileExtension("xlsx")] Xlsx,
        [FileExtension("rar")] Rar,
        [FileExtension("zip")] Zip,
        [FileExtension("7z")] Zip7,
        [FileExtension("gzip")] Gzip,
        [FileExtension("bzip2")] Bzip2,
        [FileExtension("xml")] Xml,
        [FileExtension("pdf")] Pdf,
        [FileExtension("txt")] Txt,
        [FileExtension("rtf")] Rtf,
        [FileExtension("bin")] Bin,
        [FileExtension("htm")] Htm,
        [FileExtension("html")] Html,
        [FileExtension("heic")] Heic,
        [FileExtension("dib")] Dib,
        [FileExtension("jfif")] Jfif,
        [FileExtension("jpe")] Jpe,
    }
}