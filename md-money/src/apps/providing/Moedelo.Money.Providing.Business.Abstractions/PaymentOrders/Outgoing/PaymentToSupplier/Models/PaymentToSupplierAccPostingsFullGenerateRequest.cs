using Moedelo.Money.Providing.Business.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Models
{
    public class PaymentToSupplierAccPostingsFullGenerateRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// По договору 
        /// </summary>
        public long? ContractBaseId { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// Список первичных документов
        /// </summary>
        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        public bool IsMainKontragent { get; set; }
    }
}
