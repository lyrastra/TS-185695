using System;
using Moedelo.AccountingV2.Dto.FinancialOperations.Legacy;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.AccountingV2.Dto.FinancialOperations.Legasy.Outgoing
{
    public class BudgetaryPaymentOperationDto : OutgoingOperationDto
    {
        public string Kbk { get; set; }
        public string Okato { get; set; }
        public string Oktmo { get; set; }
        public int? EnvdTaxAdministrationId { get; set; }
        public BudgetaryPaymentSubtype BudgetaryPaymentSubtype { get; set; }
        public BudgetaryPaymentFoundation BudgetaryPaymentFoundation { get; set; }
        public string TypeDescription { get; set; }
        public string RecipientName { get; set; }
        public string RecipientInn { get; set; }
        public string RecipientKpp { get; set; }
        public string RecipientSettlement { get; set; }
        public int RecipientBankId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? PeriodDate { get; set; }
        public int YearPeriod { get; set; }
        public int QuarterPeriod { get; set; }
        public string TypePeriod { get; set; }
        public string DebitAccount { get; set; }
        public string CreditAccount { get; set; }
        public string CodeUin { get; set; }
        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }
        public BudgetaryPaymentType BudgetaryPaymentPeniType { get; set; }
        public BudgetaryPaymentTurn BudgetaryPaymentTurn { get; set; }

        public override string Name => FinancialOperationNames.BudgetaryPaymentOperation;
    }
}
