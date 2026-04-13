using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.Finances.Domain.Enums.Money;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class MoneyOperation
    {
        private readonly IReadOnlyCollection<Currency> rubCurrencies = new[] { Currency.RUB, Currency.RUR, Currency.Default };
        
        public long Id { get; set; }
        
        public long DocumentBaseId { get; set; }
        
        public DateTime Date { get; set; }
        
        public DateTime? ModifyDate { get; set; }
        
        public string Number { get; set; }
        
        public MoneyDirection Direction { get; set; }
        
        public int? KontragentId { get; set; }
        
        public string KontragentName { get; set; }
        
        public OperationType OperationType { get; set; }
        
        public OperationState OperationState { get; set; }
        
        public OperationKind OperationKind { get; set; }
        
        public TaxationSystemType? TaxationSystemType { get; set; }
        
        public DocumentStatus PaidStatus { get; set; }

        /// <summary>
        /// Сумма операции указанной валюте
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Сумма операции в другой валюте (для валютных операций или операций, предполагающих перевод из одной валюты в другую) 
        /// </summary>
        public decimal? TotalSum { get; set; }

        public Currency Currency { get; set; }
        
        public SettlementAccountType SettlementType { get; set; }
        
        public string Description { get; set; }
        
        // for duplicate operation
        public long? BaseOperationId { get; set; }
        
        public int? SettlementAccountId { get; set; }

        public bool IsIgnoreNumber { get; set; }

        /// <summary>
        /// Сумма операции в рублях
        /// </summary>
        public decimal RubSum => rubCurrencies.Contains(Currency) ? Sum : TotalSum.GetValueOrDefault();
    }
}