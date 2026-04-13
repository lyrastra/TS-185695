using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.CatalogV2.Dto.Kbk
{
    public class KbkNumbersRequestDto
    {
        public DateTime Date { get; set; }
        public DateTime ActualDate { get; set; }
        public List<KbkNumberType> KbkType { get; set; }
        public KbkPaymentType KbkPaymentType { get; set; }
    }
}
