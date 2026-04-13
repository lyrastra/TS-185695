using Moedelo.Requisites.Enums.Phones;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class PhoneDto
    {
        public string Number { get; set; }

        public PhoneTypes Type { get; set; }

        public bool IsConfirmed { get; set; }
    }
}