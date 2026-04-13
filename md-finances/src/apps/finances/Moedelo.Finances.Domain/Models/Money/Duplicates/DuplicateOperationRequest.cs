using System;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class DuplicateOperationRequest
    {
        public int FirmId { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string PaymentOrderNumber { get; set; }
        public string DestinationDescription { get; set; }
        public int SettlementAccountId { get; set; }
        public int? KontragentId { get; set; }
        public string ContractorInn { get; set; }
        public string ContractorSettlementAccount { get; set; }

        public DateTime StartDate => Date.AddDays(-15);
        public DateTime EndDate => Date.AddDays(15);

        public int Direction { get; set; }

        public BudgetaryPaymentType BudgetaryPaymentOtherType => BudgetaryPaymentType.Other;

        public string CurrencyPurchaseAndSaleOutgoingOperationType => "CurrencyPurchaseAndSaleOutgoingOperation";
        public string CurrencyBalanceOperationType => "CurrencyBalanceOperation";
    }
}