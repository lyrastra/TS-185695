using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Bills
{
    public class SalesBillItemsDto
    {
        /// <summary>
        /// DocumentBaseId счета
        /// </summary>
        public long BillBaseId { get; set; }

        /// <summary>
        /// Позиции счета
        /// </summary>
        public List<SalesBillItemDto> Items { get; set; }
    }
}