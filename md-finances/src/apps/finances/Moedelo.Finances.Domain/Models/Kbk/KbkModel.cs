using Moedelo.Common.Enums.Enums.KbkNumbers;

namespace Moedelo.Finances.Domain.Models.Kbk
{
    public class KbkModel
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public KbkNumberType Type { get; set; }
        
        public int AccountCode { get; set; }
    }
}