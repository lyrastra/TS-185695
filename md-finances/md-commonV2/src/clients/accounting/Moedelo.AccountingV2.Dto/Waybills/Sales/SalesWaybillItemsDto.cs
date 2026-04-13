using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Waybills.Sales
{
    public class SalesWaybillItemsDto
    {
        /// <summary>
        /// DocumentBaseId накладной на продажу
        /// </summary>
        public long SalesWaybillBaseId { get; set; }

        /// <summary>
        /// Позиции накладной
        /// </summary>
        public List<SalesWaybillItemDto> Items { get; set; }
    }
}