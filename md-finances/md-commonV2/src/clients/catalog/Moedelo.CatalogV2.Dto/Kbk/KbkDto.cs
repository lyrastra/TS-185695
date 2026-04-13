using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.CatalogV2.Dto.Kbk
{
    public class KbkDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public KbkNumberType Type { get; set; }
        
        public int AccountCode { get; set; }
    }
}