using System;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class DividendPaymentOperationDto : OutgoingOperationDto
    {
        public DateTime DateOfDocument { get; set; }

        public override string Name => FinancialOperationNames.DividendPaymentOperation;
    }
}
