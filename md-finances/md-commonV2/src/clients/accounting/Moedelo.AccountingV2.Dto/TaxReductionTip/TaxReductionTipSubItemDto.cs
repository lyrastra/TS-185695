using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.TaxReductionTip
{
    public class TaxReductionTipSubItemDto
    {
        /// <summary>
        /// DocumentBaseId входящего документа
        /// </summary>
        public long BaseId { get; set; }

        /// <summary>
        /// Тип входящего документа (накладная или УПД)
        /// </summary>
        public AccountingDocumentType Type { get; set; }

        /// <summary>
        /// Номер входящего документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата входящего документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма позиций входящего документа
        /// </summary>
        public decimal DocumentSum { get; set; }

        /// <summary>
        /// Оплачено поставщику
        /// </summary>
        public decimal PaymentSum { get; set; }

        /// <summary>
        /// Сумма оплаты (на сколько осталось оплатить)
        /// </summary>
        public decimal NewPaymentSum { get; set; }

        /// <summary>
        /// Учтётся в расходы (на сколько увеличится сумма налоговых проводок)
        /// </summary>
        public decimal ProfitSum { get; set; }

        /// <summary>
        /// Процент проданного товара
        /// </summary>
        public decimal SalePercent { get; set; }
    }
}
