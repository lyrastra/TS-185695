using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Billing.Abstractions.PaymentLink.Dto
{
    public class PaymentLinkRequestDto
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Номер счёта
        /// </summary>
        public string BillNumber { get; set; }
        public int PaymentHistoryId { get; set; }
        /// <summary>
        /// Название услуги
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal Sum { get; set; }
        /// <summary>
        /// Срок действия услуги
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
        /// <summary>
        /// Срок действия счёта
        /// </summary>
        public DateTime? BillExpirationDate { get; set; }
        /// <summary>
        /// Название / имя регионального партнера
        /// </summary>
        public string RegionalPartnerName { get; set; }
        /// <summary>
        /// Флаг АУТа
        /// </summary>
        public bool IsOutsource { get; set; }
    }
}
