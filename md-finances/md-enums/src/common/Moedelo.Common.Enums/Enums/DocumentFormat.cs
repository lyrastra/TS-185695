using Moedelo.Common.Enums.Attributes;

namespace Moedelo.Common.Enums.Enums
{
    public enum DocumentFormat
    {
        [FileExtension("xls")]
        Xls = 0,

        [FileExtension("pdf")]
        Pdf = 1,

        [FileExtension("doc")]
        Doc = 2,

        [FileExtension("xlsx")]
        Xlsx = 3,

        [FileExtension("xml")]
        Xml = 4,

        [FileExtension("txt")]
        Txt = 5,

        [FileExtension("rtf")]
        Rtf = 6,

        [FileExtension("zip")]
        Zip = 7,

        [FileExtension("csv")]
        Csv = 8
    }
}