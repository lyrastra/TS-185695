using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Billing.Abstractions.PaymentLink.Dto
{
    public class PaymentLinkDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }
        public Guid Guid { get; set; }
        /// <summary>
        /// Дата\время создания ссылки
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Дата\время истечения действия ссылки
        /// </summary>
        public DateTime ExpiryDate { get; set; }
        /// <summary>
        /// Номер счёта
        /// </summary>
        public string BillNumber { get; set; }
        public int PaymentHistoryId { get; set; }
        public BillInfo Data { get; set; }
        /// <summary>
        /// Флаг истечения срока действия ссылки
        /// </summary>
        public bool IsExpired { get; set; }
    }

    /// <summary>
    /// Содержимое поля data_json после парсинга записи PaymentLink
    /// </summary>
    public class BillInfo
    {
        /// <summary>
        /// Название услуги
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal Sum { get; set; }
        /// <summary>
        /// Срок действия
        /// </summary>
        public DateTime? BillExpirationDate { get; set; }
        /// <summary>
        /// Название / имя регионального партнера
        /// </summary>
        public string RegionalPartnerName { get; set; }
    }
}
