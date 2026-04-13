using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.HomeV2.Client.Phone.Models
{
    public class FakePhoneCreateRequestDto
    {
        public int FirmId { get; set; }

        public PhoneTypes Type { get; set; }
    }
}