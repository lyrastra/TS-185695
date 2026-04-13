using Moedelo.Common.Enums.Enums.Email;

namespace Moedelo.SpamV2.Dto.MailSender.EmailTable
{
    public class FirmEmailDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public string EmailAddress { get; set; }

        public EmailType EmailType { get; set; }
    }
}
