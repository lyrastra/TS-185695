using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BillingV2.Dto.BackofficeBilling.Bills.BillRequest
{
    public class BillRequestDto
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Флаг технического платежа
        /// </summary>
        public bool IsTechnicalBill { get; set; }

        /// <summary>
        /// Метод оплаты
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Промокод
        /// </summary>
        public string PromoCode { get; set; }

        /// <summary>
        /// Набор продуктовых услуг в счёте
        /// </summary>
        public IReadOnlyCollection<ProductConfigurationRequestDto> ProductConfigurations { get; set; }

        /// <summary>
        /// Источник создания счёта (интерфейс выставления счетов, Маркетплейс и прочие)
        /// </summary>
        public BillCreationSource CreationSource { get; set; }

        /// <summary>
        /// Признак допродажи
        /// </summary>
        public bool IsCrossSelling { get; set; }
    }
}
