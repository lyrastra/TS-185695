using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.PayrollV2.Dto.SalaryPayments
{
    public class PaymentForAccountablePersonDto
    {
        public string Number { get; set; }

        public string Date { get; set; }

        public AccountingDocumentType Type { get; set; }

        public AdvancePaymentDocumentTypes AdvancePaymentType { get; set; }

        public int WorkerId { get; set; }

        public string WorkerName { get; set; }

        public decimal Sum { get; set; }
    }
}
