using System;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class FileExtractDto
    {
        public string SettlementAccountNumber { get; set; }

        public bool IsSettlementAccountExist { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
