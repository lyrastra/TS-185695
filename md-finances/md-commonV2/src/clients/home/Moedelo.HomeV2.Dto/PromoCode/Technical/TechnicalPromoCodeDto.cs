using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.HomeV2.Dto.PromoCode.Technical
{
    public class TechnicalPromoCodeDto
    {
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
        public decimal FixedOutputSum { get; set; }

        /// <summary>
        /// Количество месяцев, добавляемое бонусом к сроку оплаты. 
        /// </summary>
        public int MonthsAsBonus { get; set; }

        public string Description { get; set; }

        /// <summary> Тип купона. </summary>
        public PromoCodeType PromoCodeType { get; set; }

        /// <summary>
        /// Тип выгодного предложения по промокоду
        /// </summary>
        public PromoCodeOfferType OfferType { get; set; }

        /// <summary>
        /// Сумма, на которую уменьшается оплата тарифа
        /// </summary>
        public int DiscountSum { get; set; }

        /// <summary> Фирма активировавшая промо-код. </summary>
        public int FirmId { get; set; }
    }
}
