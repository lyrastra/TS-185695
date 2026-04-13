using Moedelo.Common.Enums.Enums.Money;
using System;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Finances.Domain.Models.Money.Operations.Duplicates
{
    public class OperationDuplicateForBatchCheck
    {
        public long Id { get; set; }
        public long DocumentBaseId { get; set; }
        public MoneyDirection Direction { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Description { get; set; }
        public string ContractorSettlementAccount { get; set; }
        public string ContractorInn { get; set; }
        public bool IsSalaryOperation { get; set; }
        public bool IsProfitWithdrawingOperation { get; set; }
        public OperationType? OperationType { get; set; }
    }
}