using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Money
{
    public class KontragentForMoneyReplaceDto
    {
        public List<KontragentForMoneyReplaceItemDto> Items { get; set; }

        public DateTime StartDate { get; set; }
    }
}
