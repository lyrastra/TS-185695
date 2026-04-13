using System;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class BillLinkDto
    {
        /// <summary>
        /// Идентификатор счёта
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата счёта
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер счёта
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма счёта
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