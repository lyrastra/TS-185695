using Moedelo.Finances.Enums;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto
{
    public class KbkDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public KbkNumberType Type { get; set; }
        
        public int AccountCode { get; set; }
    }
}