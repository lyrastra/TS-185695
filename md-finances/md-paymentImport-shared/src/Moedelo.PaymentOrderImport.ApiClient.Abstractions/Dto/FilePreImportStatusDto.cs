using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class FilePreImportStatusDto
    {

        public PaymentImportResultStatus Status { get; set; }

        public FileExtractDto Extract { get; set; }

        public string Description => Status.GeDescription();
    }
}
