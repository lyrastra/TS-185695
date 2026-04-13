using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.PostingEngine;
using System;

namespace Moedelo.AccountingV2.Dto.CashOrder
{
    public class FirmCashOrderDto
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public long CashId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public OperationType OperationType { get; set; }

        public PaymentDirection Direction { get; set; }

        public decimal PaidCardSum { get; set; }

        public long KontragentId { get; set; }

        public int? KontragentAccountCode { get; set; }

        public long? ContractBaseId { get; set; }
        
        /// <summary>
        /// Номер z отчета ПКО в ОРП
        /// </summary>
        public string ZReportNumber { get; set; }

        public int? BudgetaryTaxesAndFees { get; set; }

        public int? KbkId { get; set; }
    }
}