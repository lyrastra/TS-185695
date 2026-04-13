using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class RefundFromEmployeeIncomingOperationDto : IncomingOperationDto
    {
        public int? WorkerId { get; set; }

        public override string Name => FinancialOperationNames.RefundFromEmployeeIncomingOperation;
    }
}
