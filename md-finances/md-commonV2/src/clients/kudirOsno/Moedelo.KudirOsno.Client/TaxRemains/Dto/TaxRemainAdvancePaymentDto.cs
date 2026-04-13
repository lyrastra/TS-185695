using Moedelo.Common.Enums.Enums.Ndfl;

namespace Moedelo.KudirOsno.Client.TaxRemains.Dto
{
    /// <summary>
    /// Модель остатков авансовых платежей
    /// </summary>
    public class TaxRemainAdvancePaymentDto
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
