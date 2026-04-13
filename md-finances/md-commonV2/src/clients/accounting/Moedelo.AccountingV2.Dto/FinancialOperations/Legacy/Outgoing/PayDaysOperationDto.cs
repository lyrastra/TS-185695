using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class PayDaysOperationDto : OutgoingOperationDto
    {
        public int? WorkerId { get; set; }
        public decimal FssSum { get; set; }
        public PayDaysPaymentType PaymentType { get; set; }
        public string BankSettlementAccount { get; set; }
        public decimal PatentSum { get; set; }

        public override string Name => FinancialOperationNames.PayDaysOperation;
    }
}
