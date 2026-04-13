using System;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto
{
    public class ContractDto
    {
        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата договора
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер договора
        /// </summary>
        public string Number { get; set; }
    }
}