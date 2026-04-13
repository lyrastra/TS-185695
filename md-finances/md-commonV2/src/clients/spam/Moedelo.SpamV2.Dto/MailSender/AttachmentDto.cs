namespace Moedelo.SpamV2.Dto.MailSender
{
    public class AttachmentDto
    {
        /// <summary>
        /// Base64 содержимое вложения
        /// </summary>
        public string Content { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }
    }
}
