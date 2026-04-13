
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class StartBPMImportResultDto
    {
        public PaymentImportResultStatus Status { get; set; }
        public string Description { get; set; }
        public FileExtractDto FileExtract { get; set; }
        public long? ImportId { get; set; }
    }
}
