using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class UkInpamentOperationDto : IncomingOperationDto
    {
        public override string Name => FinancialOperationNames.UkInpamentOperation;
    }
}
