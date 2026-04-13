using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Cashier
{
    public class CashierCollectionDto
    {
        /// <summary>
        /// Общее колличество касс
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Список касс
        /// </summary>
        public List<CashierDto> ResourceList { get; set; }
    }
}