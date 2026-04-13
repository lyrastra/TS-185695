using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class RefundToCustomerOutgoingOperationDto : OutgoingOperationDto
    {
        public string ReturnWaybill { get; set; }
        public decimal PatentSum { get; set; }

        public override string Name => FinancialOperationNames.RefundToCustomerOutgoingOperation;
    }
}
