using System;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class DocumentLinkDto
    {
        /// <summary>
        /// Идентификатор первичного документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата первичного документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер первичного документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма первичного документа
        /// </summary>
        public decimal DocumentSum { get; set; }

        /// <summary>
        /// Учитываемая сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма, оплаченная в другом платежном документе
        /// </summary>
        public decimal PaidSum { get; set; }
    }
}