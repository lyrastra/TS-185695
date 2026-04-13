using Moedelo.Common.Enums.Enums.PaymentImport;

namespace Moedelo.Finances.Domain.Models.PaymentImport
{
    public class ImportStatus
    {
        public object ExData { get; set; }

        public ImportResultStatus Status { get; set; }

        public string FileId { get; set; }
    }
}
