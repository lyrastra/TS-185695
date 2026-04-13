using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.HomeV2.Dto.PromoCode
{
    public class PromoCodeInfoDto
    {
        public int Id { get; set; }

        /// <summary> Дата создания/последнего изменения </summary>
        public DateTime? LastChangeDate { get; set; }

        /// <summary> Дата начала действия </summary>
        public DateTime? StartDate { get; set; }

        /// <summary> Дата окончания действия </summary>
        public DateTime? EndDate { get; set; }

        /// <summary> Для какого периода действует (-1 - для всех) </summary>
        public int Period { get; set; }

        /// <summary> Для какого тарифа действует (название) </summary>
        public string Tariff { get; set; }

        public List<Tariff> Tariffs { get; set; }

        /// <summary> Наименование купона/промо-кода </summary>
        public string Name { get; set; }

        /// <summary> Процент скидки </summary>
        public int PercentRate { get; set; }

        /// <summary> Фиксированная сумма оплаты после применения кода </summary>
        public int FixedOutputSumm { get; set; }

        /// <summary> ID пользователя, создавшего код </summary>
        public int CreatorUserId { get; set; }

        /// <summary> Логин пользователя, создавшего код </summary>
        public string CreatorLogin { get; set; }

        /// <summary> Описание </summary>
        public string Description { get; set; }

        /// <summary> Количество месяцев в подарок </summary>
        public int MonthesAsBonus { get; set; }

        /// <summary> Фиксированная дата завершения оплаты </summary>
        public DateTime? ExpirationDateAsBonus { get; set; }

        /// <summary> Тип: купон/промо-код </summary>
        public string PromoCodeType { get; set; }

        /// <summary> Количество оплат по коду </summary>
        public int UsageCount { get; set; }

        /// <summary> Дата активации промо-кода</summary>
        public DateTime? ActivateDate { get; set; }

        /// <summary> Логин активировавшего пользователя </summary>
        public string LoginConsumer { get; set; }

        /// <summary>Id источника, откуда приходит клиент: функционал автоскидок</summary>
        public int? ClientSourceId { get; set; }

        /// <summary> Список разрешенных операций при выставлении счета </summary>
        public BillingOperationType[] BillingOperations { get; set; }

        /// <summary>
        /// Действие промокода только для основной позиции счета
        /// не работает для типов промокодов "месяцы в подарок" и "дата окончания до"
        /// </summary>
        public bool IsForMainBillItem { get; set; }
    }
}
