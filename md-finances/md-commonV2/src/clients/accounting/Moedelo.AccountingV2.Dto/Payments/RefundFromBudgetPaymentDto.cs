using Moedelo.Common.Enums.Enums.Payroll;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class RefundFromBudgetPaymentDto
    {
        public long Id { get; set; }

        public decimal Sum { get; set; }

        public string OrderNumber { get; set; }

        public string OrderDate { get; set; }

        public PaysForEmployeeType PaymentType { get; set; }

        public long? DocumentBaseId { get; set; }

        public string DocumentTypeDescription { get; set; }
    }
}
