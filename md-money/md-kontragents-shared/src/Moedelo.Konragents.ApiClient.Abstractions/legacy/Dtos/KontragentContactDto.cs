using System.Collections.Generic;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public class KontragentContactDto
    {
        public long Id { get; set; }

        public int KontragentId { get; set; }

        public string Fio { get; set; }

        public IReadOnlyCollection<ContactEmailDto> Emails { get; set; }

        public IReadOnlyCollection<ContactPhoneDto> Phones { get; set; }

        public string Skype { get; set; }
    }
}