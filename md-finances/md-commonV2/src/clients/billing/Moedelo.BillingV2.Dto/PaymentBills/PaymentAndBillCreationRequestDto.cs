using System;
using System.Collections.Generic;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.BillingV2.Dto.Billing.PaymentPositions;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BillingV2.Dto.PaymentBills
{
    public class PaymentAndBillCreationRequestDto
    {
        /// <summary>
        /// уникальный идентификатор заявки на создание платежа и счёта
        /// </summary>
        public Guid CreationRequestGuid { get; set; }
        /// <summary>
        /// идентификатор фирмы, по которой выставляется платёж
        /// </summary>
        public int FirmId { get; set; }
        /// <summary>
        /// дата проведения платежа
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// способ оплаты
        /// </summary>
        public string PaymentMethod { get; set; }
        /// <summary>
        /// идентификатор технического прайс-листа (определяющего набор прав), по которому выставляется платёж
        /// </summary>
        public int TechnicalPriceListId { get; set; }
        /// <summary>
        /// идентификатор коммерческого прайс-листа, по которому выставляется платёж
        /// этот прайс-лист не влияет на набор прав пользователя,
        /// он используется в коммерческих целях
        /// может отсутствовать, тогда в его качестве используется технический
        /// </summary>
        public int? CommercialPriceListId { get; set; }
        /// <summary>
        /// сумма платежа
        /// </summary>
        public decimal Sum { get; set; }
        /// <summary>
        /// сумма предоставленной скидки
        /// </summary>
        public decimal DiscountSum { get; set; }
        /// <summary>
        /// идентификатор применённого промо-кода (0 - платёж без промо-кода)
        /// </summary>
        public int PromoCodeId { get; set; }
        /// <summary>
        /// идентификатор пользователя-продавца, за которым должен быть закреплён этот платёж
        /// </summary>
        public int SellerId { get; set; }
        /// <summary>
        /// платёж должен быть создан с выставленным флагом успешности (="Оплачено")
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// дата начала действия платежа
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// дата окончания действия платежа (исторически nullable, но на практике должна быть заполнена обязательно)
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// признак "Допродажа"
        /// В реальности имеет весьма странную логику как проставления, так и интерпретации:
        /// это не то, чтобы "допродажа" а скорее "Не учитывать в переподписке" (с натяжкой тоже)
        /// </summary>
        public bool IsUpsell { get; set; }
        /// <summary>
        /// идентификатор пользователя продавца, являющегося сотрудником регионального партнёра
        /// (если платёж "привязан к партнёру", то совпадает с SellerId) 
        /// </summary>
        public int RegionalPartnerSellerUserId { get; set; }
        /// <summary>
        /// заметка по платежу
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// тип операции
        /// </summary>
        public BillingOperationType OperationType { get; set; }
        /// <summary>
        /// канал продаж? по факту в 99%+ платежей стоит 1
        /// </summary>
        public int SalesChannel { get; set; }
        /// <summary>
        /// номер счёта
        /// </summary>
        public string BillNumber { get; set; }
        /// <summary>
        /// дата счёта
        /// </summary>
        public DateTime BillDate { get; set; }
        /// <summary>
        /// Отметка "аутсорс" для счёта
        /// </summary>
        public bool IsBillOutsource { get; set; }
        /// <summary>
        /// Дополнительные поля счёта
        /// </summary>
        public PaymentHistoryExBillDataDto BillData { get; set; }
        /// <summary>
        /// позиции платежа (счёта)
        /// </summary>
        public IReadOnlyCollection<PaymentPositionDto> Positions { get; set; }
    }
}