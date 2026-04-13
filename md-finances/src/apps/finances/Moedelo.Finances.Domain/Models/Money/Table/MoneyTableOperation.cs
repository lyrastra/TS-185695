using System;
using System.Collections.Generic;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;

namespace Moedelo.Finances.Domain.Models.Money.Table
{
    public class MoneyTableOperation : MoneyOperation
    {
        /// <summary>
        /// Состояние выставленого счета клиенту в банке
        /// </summary>
        public PassThruPaymentState PassThruPaymentState { get; set; }
        
        // for db load only
        public int TotalCount { get; set; }

        // for taxes sums
        public IReadOnlyCollection<TaxSumRec> Taxes { get; set; } = Array.Empty<TaxSumRec>();

        // for salary charges
        public bool HasUnbindedSalaryChargePayments { get; set; }

        public PrimaryDocsStatus PrimaryDocStatus { get; set; }

        public int LinkedDocumentsCount { get; set; }

        public decimal UncoveredSum { get; set; }

        /// <summary>
        /// Применённые правила импорта 
        /// </summary>
        public IReadOnlyCollection<MoneyTableOperationImportRule> ImportRules { get; set; } = Array.Empty<MoneyTableOperationImportRule>();

        /// <summary>
        /// Применённые массовые правила импорта из аутсорса
        /// </summary>
        public IReadOnlyCollection<MoneyTableOperationImportRule> OutsourceImportRules { get; set; } = Array.Empty<MoneyTableOperationImportRule>();

        public bool CanDownload { get; set; }

        public bool CanSendToBank { get; set; }

        public bool OutsourceIsApproved =>
            OperationState == Common.Enums.Enums.Finances.Money.OperationState.OutsourceApproved;
    }
}