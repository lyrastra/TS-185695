using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BackofficeV2.Dto.CreateBill
{
    public class PriceListCalculationDto
    {
        //todo возможно, здесь есть лишние и неиспользуемые поля

        /// <summary>
        /// Длительность текущего оплаченного периода (с учётом подарков и т.п.) 
        /// </summary>
        public int CurrentMonthCount { get; set; }

        /// <summary>
        /// Длительность периода нового тарифа
        /// </summary>
        public int PriceListMonthCount { get; set; }

        /// <summary>
        /// Длительность периода нового тарифа (с учётом подарков и т.п.) 
        /// </summary>
        public int NewMonthCount { get; set; }

        /// <summary>
        /// Бонусные месяцы
        /// </summary>
        public int MonthsAsBonus { get; set; }
        
        /// <summary>
        /// Дата начала действия нового тарифа
        /// </summary>
        public DateTime NewStartDate { get; set; }
        
        /// <summary>
        /// Дата окончания действия нового тарифа
        /// </summary>
        public DateTime NewExpirationDate { get; set; }
        
        /// <summary>
        /// Промо-код для нового тарифа
        /// </summary>
        public string NewPromoCode { get; set; }

        /// <summary>
        /// Признак успешности применения промокода
        /// </summary>
        public bool HasPromoCodeApplied { get; set; }
        
        /// <summary>
        /// Идентификатор нового прайс-листа
        /// </summary>
        public int NewPriceListId { get; set; }

        public int TariffId  { get; set; }
        
        public BillingOperationType OperationType { get; set; }

        
        /// <summary>
        /// Полная стоимость нового тарифа
        /// </summary>
        public decimal NewFullPrice { get; set; }
        
        /// <summary>
        /// Остаток суммы по действующему оплаченному тарифу с учётом прошедшего времени
        /// </summary>
        public decimal CurrentPaidSumRemain { get; set; }
        
        /// <summary>
        /// Скидка по региональному коэффициенту
        /// </summary>
        public decimal RegionalDiscount { get; set; }

        /// <summary>
        /// Итоговая сумма с учетом всех скидок
        /// </summary>
        public decimal Price { get; set; }
        
        
        public List<Position> Positions { get; set; }

        public class Position
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public PaymentPositionType Type { get; set; }
            //todo удалить
            public decimal RegionalRatio { get; set; }
            public bool HasNds { get; set; }
            
            /// <summary>
            /// нормативная цена (до применения скидок и региональных коэффициентов)
            /// </summary>
            public decimal? NormativePrice { get; set; }
        }
    }
}