using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class PaymentsForReportDto
    {
        public PaymentsForReportDto()
        {
            Payments = new List<FundPaymentForReportDto>();
        }

        public List<FundPaymentForReportDto> Payments { get; set; }
    }
}