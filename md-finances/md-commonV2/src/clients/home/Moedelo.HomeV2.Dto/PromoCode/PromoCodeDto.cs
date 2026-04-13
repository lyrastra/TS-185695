using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.HomeV2.Dto.PromoCode
{
    public class PromoCodeDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Дата создания или последнего изменения купона. 
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Дата начала вступления в силу купона. 
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата завершения действия купона. 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Длительность покупаемого тарифа, при которой купон валиден. 
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Имя купона. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Размер процентной скидки, предоставляемой купоном. 
        /// </summary>
        public int PercentRate { get; set; }

        /// <summary>
        /// Фиксированная выходная сумма, предоставляемая купоном. 
        /// </summary>
        public int FixedOutputSum { get; set; }

        /// <summary>
        /// Идентификатор создателя купона. 
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Количество месяцев, добавляемое бонусом к сроку оплаты. 
        /// </summary>
        public int MonthesAsBonus { get; set; }

        /// <summary>
        /// Дата истечения тарифа при оплате, устанавливаемая бонусом. 
        /// </summary>
        public DateTime? ExpirationDateAsBonus { get; set; }

        /// <summary> Описание купона. </summary>
        public string Description { get; set; }

        /// <summary> Тип купона. </summary>
        public PromoCodeType PromoCodeType { get; set; }

        public List<Tariff> Tariffs { get; set; }

        public bool IsExclusive { get; set; }

        public int? MrkActionId { get; set; }

        /// <summary>
        /// Тип выгодного предложения по промокоду
        /// </summary>
        public PromoCodeOfferType OfferType { get; set; }

        /// <summary>
        /// Сумма, на которую уменьшается оплата тарифа
        /// </summary>
        public int DiscountSum { get; set; }

        /// <summary> Фирма активировавшая промо-код. </summary>
        public int? FirmId { get; set; }

        /// <summary>Id источника, откуда приходит клиент: функционал автоскидок</summary>
        public int? ClientSourceId { get; set; }

        public string LoginConsumer { get; set; }

        public string CreatorLogin { get; set; }

        public int UsageCount { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public string Tariff { get; set; }

        /// <summary> Список разрешенных операций при выставлении счета </summary>
        public BillingOperationType[] BillingOperations { get; set; }

        /// <summary>
        /// Действие промокода только для основной позиции счета
        /// не работает для типов промокодов "месяцы в подарок" и "дата окончания до"
        /// </summary>
        public bool IsForMainBillItem { get; set; }
    }
}