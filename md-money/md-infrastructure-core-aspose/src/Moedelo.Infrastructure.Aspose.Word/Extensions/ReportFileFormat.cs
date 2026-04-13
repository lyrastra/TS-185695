using Aspose.Words;
using Moedelo.Infrastructure.Aspose.Word.Enums;

namespace Moedelo.Infrastructure.Aspose.Word.Extensions
{
    public static class ReportFileFormat
    {
        public static string ToFileExtension(this PrintFormType format)
        {
            switch (format)
            {
                case PrintFormType.Docx: return ".docx";
                case PrintFormType.Pdf: return ".pdf";
                case PrintFormType.Html: return ".html";
                case PrintFormType.Doc: return ".doc";
            }

            return string.Empty;
        }
        
        public static SaveFormat ToAsposeSaveFormat(this PrintFormType format)
        {
            switch (format)
            {
                case PrintFormType.Docx: return SaveFormat.Docx;
                case PrintFormType.Pdf: return SaveFormat.Pdf;
                case PrintFormType.Html: return SaveFormat.Html;
                case PrintFormType.Doc: return SaveFormat.Doc;
            }

            return SaveFormat.Unknown;
        }
        
        public static string ToMimeType(this PrintFormType format)
        {
            switch (format)
            {
                case PrintFormType.Docx: return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case PrintFormType.Pdf: return "application/pdf";
                case PrintFormType.Html: return "text/html";
                case PrintFormType.Doc: return "application/msword";
            }

            return string.Empty;
        }
    }
}