using Moedelo.Common.Enums.Enums.PaymentImport;

namespace Moedelo.PaymentImport.Dto
{
    public class ImportStatusDto
    {
        public ImportResultStatus Status { get; set; }

        public object ExData { get; set; }
    }
}
