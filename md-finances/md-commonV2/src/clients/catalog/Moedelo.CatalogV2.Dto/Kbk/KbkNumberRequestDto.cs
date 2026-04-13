using System;
using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.CatalogV2.Dto.Kbk
{
    public class KbkNumberRequestDto
    {
        public DateTime Date { get; set; }
        public DateTime ActualDate { get; set; }
        public KbkNumberType KbkType { get; set; }
        public KbkPaymentType KbkPaymentType { get; set; }
    }
}
