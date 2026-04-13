using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BackofficeV2.Dto.Payments
{
    /// <summary> Информация о фирме для фрейма </summary>
    public class PriceListCalculationRequestDto
    {
        /// <summary> Идентификатор фирмы </summary>
        public int FirmId { get; set; }

        /// <summary> Тип операции </summary>
        public BillingOperationType OperationType { get; set; }

        /// <summary> Идентификатор прайс-листа, для которого запрашивается расчет цены </summary>
        public int PriceListId { get; set; }

        /// <summary> Промо-код </summary>
        public string PromoCodeName { get; set; }
        
        /// <summary>
        /// Id региона для оплаты
        /// </summary>
        public int PayRegionId { get; set; }

        /// <summary> 
        /// Фиксированная стоимость, указанная при создании платежа,
        /// промокоды и региональные коэффициенты НЕ ПРИМЕНЯЮТСЯ 
        /// </summary>
        public decimal? FixedPrice { get; set; }
    }
}