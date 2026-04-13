using System.Collections.Generic;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.MailSender
{
    public class EmailDto
    {
        public string FromAddress { get; set; }

        public string FromName { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public IList<string> Addresses { get; set; }

        public IList<string> ReplyToAdresses { get; set; }

        public IList<string> CopyToAdresses { get; set; }
        
        public IList<string> HiddenCopyToAdresses { get; set; }

        public IList<AttachmentDto> Attachments { get; set; }

        public int ProductPartition { get; set; }

        /// <summary>Идентификатор почтовой рассылки(не воспринимает кириллицу): https://yandex.ru/support/postoffice/advices.html</summary>
        public string ListId { get; set; }

        /// <summary> обычная строка или json индивидуальные параметры каждого WL </summary>
        public string WLSpecialParams { get; set; }
                
        public string InReplyTo { get; set; }
        
        public IList<string> References { get; set; }

        public string MessageId { get; set; }
    }
}