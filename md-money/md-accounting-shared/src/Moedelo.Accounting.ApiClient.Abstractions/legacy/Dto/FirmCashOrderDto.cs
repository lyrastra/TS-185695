using System;
using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
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
        
        /// <summary>
        /// Номер z отчета ПКО в ОРП
        /// </summary>
        public string ZReportNumber { get; set; }
    }
}