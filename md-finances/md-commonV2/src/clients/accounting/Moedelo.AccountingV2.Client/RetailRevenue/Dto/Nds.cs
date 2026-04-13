using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.AccountingV2.Client.RetailRevenue.Dto
{
    public class Nds
    {
        /// <summary>
        /// Сумма НДС (в т. ч.)
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Ставка НДС
        /// </summary>
        public NdsTypes Rate { get; set; }
    }
}