namespace Moedelo.SpamV2.Dto.MailSender
{
    public class BaseEmailResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string LabelForIdentification { get; set; }
    }
}