using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Docs.Dto.RetailRefunds
{
    public class RetailRefundDto
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int KontragentId { get; set; }

        public long StockId { get; set; }

        public long RetailReportDocumentBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public TaxationSystemType TaxSystem { get; set; }

        /// <summary>
        /// Номер заявления
        /// </summary>
        public string RequestNumber { get; set; }

        /// <summary>
        /// Дата заявления
        /// </summary>
        public DateTime? RequestDate { get; set; }

        /// <summary>
        /// Платеж на возврат
        /// </summary>
        public RetailRefundPaymentDto RefundPayment { get; set; }
        
        /// <summary>
        /// Сумма возврата в рублях
        /// </summary>
        public decimal Sum { get; set; }
        
        public List<RetailRefundItemDto> Items { get; set; }
        
    }
}