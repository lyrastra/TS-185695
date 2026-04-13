using Moedelo.Common.Enums.Enums.Ndfl;

namespace Moedelo.TaxRemains.Client.Dto
{
    /// <summary>
    /// Модель остатков авансовых платежей у ИП ОСНО
    /// </summary>
    public class TaxRemainIpOsnoAdvancePaymentDto
    {
        /// <summary>
        /// Номер квартала
        /// </summary>
        public int Quarter { get; set; }

        /// <summary>
        /// Ставка по НДФЛ
        /// </summary>
        public TaxRateMode TaxRateMode { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>  
        public decimal Sum { get; set; }
    }
}
