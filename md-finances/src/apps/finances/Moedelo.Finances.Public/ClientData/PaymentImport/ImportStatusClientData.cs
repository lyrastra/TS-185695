using Moedelo.Common.Enums.Enums.PaymentImport;

namespace Moedelo.Finances.Public.ClientData.PaymentImport
{
    public class ImportStatusClientData
    {
        public object ExData { get; set; }

        public ImportResultStatus Status { get; set; }

        public string FileId { get; set; }
    }
}