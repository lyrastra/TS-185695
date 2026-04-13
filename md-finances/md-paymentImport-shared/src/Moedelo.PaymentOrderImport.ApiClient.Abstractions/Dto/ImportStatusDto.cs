using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class ImportStatusDto
    {

        public PaymentImportResultStatus Status { get; set; }

        public object ExData { get; set; }

        public string Description => Status.GeDescription();
    }
}
