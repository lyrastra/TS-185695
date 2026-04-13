using System;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class SberbankStatementSummaryResponseDto
    {
        public AmountFintech ClosingBalance { get; set; }

        public AmountFintech ClosingBalanceRub { get; set; }

        public DateTime ComposedDateTime { get; set; }

        public int CreditTransactionsNumber { get; set; }

        public AmountFintech CreditTurnover { get; set; }

        public AmountFintech CreditTurnoverRub { get; set; }

        public int DebitTransactionsNumber { get; set; }

        public AmountFintech DebitTurnover { get; set; }

        public AmountFintech DebitTurnoverRub { get; set; }

        public string LastMovementDate { get; set; }

        public AmountFintech OpeningBalance { get; set; }

        public AmountFintech OpeningBalanceRub { get; set; }

        public double? OpeningRate { get; set; }
    }
}
