using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.PriceLists.Dto.PriceLists
{
    public class PriceListPositionDto
    {
        public PaymentPositionType Type { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Заблокировано изменение суммы при выставлении счёта
        /// </summary>
        public bool IsSumLocked { get; set; }
    
        public bool HasNds { get; set; }
    
        public TariffProduct Product { get; set; }
    }
}