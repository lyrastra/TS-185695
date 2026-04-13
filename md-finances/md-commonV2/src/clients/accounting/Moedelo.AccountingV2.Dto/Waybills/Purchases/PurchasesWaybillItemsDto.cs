using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Waybills.Purchases
{
    public class PurchasesWaybillItemsDto
    {
        /// <summary>
        /// DocumentBaseId накладной на продажу
        /// </summary>
        public long PurchasesWaybillBaseId { get; set; }

        /// <summary>
        /// Позиции накладной
        /// </summary>
        public List<PurchasesWaybillItemDto> Items { get; set; }
    }
}