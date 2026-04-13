using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.CommonV2.EventBus.Registration
{
    public class AddTrialPaymentCommand
    {
        public int TrialCardId { get; set; }

        /// <summary>
        /// Тариф, обязателен если не передается PriceListId
        /// </summary>
        public Tariff Tariff { get; set; }

        /// <summary>
        /// Период тарифа
        /// </summary>
        public int? TariffPeriod { get; set; }

        /// <summary>
        /// PriceListId, обязателен если не передается Tariff
        /// </summary>
        public int PriceListId { get; set; }

        /// <summary>
        /// ID коммерческого прайс-листа для триальных аутсорс-платежей
        /// </summary>
        public int? CommercialPriceListId { get; set; }

        public DateTime PaymentStartDate { get; set; }

        public int FirmId { get; set; }

        public int ReferalId { get; set; }
        
        /// <summary>
        /// Кол-во дней триального доступа
        /// </summary>
        public int AccessDays { get; set; }
    }
}