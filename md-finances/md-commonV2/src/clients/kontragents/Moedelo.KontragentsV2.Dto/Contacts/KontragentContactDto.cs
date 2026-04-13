using System.Collections.Generic;

namespace Moedelo.KontragentsV2.Dto.Contacts
{
    public class KontragentContactDto
    {
        public long Id { get; set; }

        public int KontragentId { get; set; }

        public string Fio { get; set; }

        public List<ContactEmailDto> Emails { get; set; }

        public List<ContactPhoneDto> Phones { get; set; }

        public string Skype { get; set; }
    }
}