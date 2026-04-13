using System;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Incoming
{
    public class RefundFromBudgetIncomingOperationDto : IncomingOperationDto
    {
        public string Kbk { get; set; }
        public BudgetaryPaymentSubtype BudgetaryPaymentSubtype { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string TypeDescription { get; set; }
        public string RecipientName { get; set; }
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }

        public override string Name => FinancialOperationNames.RefundFromBudgetIncomingOperation;
    }
}
