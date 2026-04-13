using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.SpamV2.Dto.MailSender
{
    public class EmailDto
    {
        /// <summary>
        /// Адрес отправителя
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// Отображаемое имя отправителя
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Заголовок письма
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело письма
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Тело письма - HTML
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Получатели
        /// </summary>
        [Required]
        public IList<string> Addresses { get; set; }

        /// <summary>
        /// Кому направлять ответы на данное письмо
        /// </summary>
        public IList<string> ReplyToAdresses { get; set; }

        /// <summary>
        /// Получатели копии
        /// </summary>
        public IList<string> CopyToAdresses { get; set; }

        /// <summary>
        /// Получатели скрытой копии
        /// </summary>
        public IList<string> HiddenCopyToAdresses { get; set; }

        /// <summary>
        /// Вложения
        /// </summary>
        public IList<AttachmentDto> Attachments { get; set; }

        [Obsolete("Не используется")]
        public MailRequestV2Dto MailRequestV2 { get; set; }

        /// <summary>
        /// Продукт клиента
        /// Используется для брендирования, можно получить из IProductPartitionApiClient
        /// Важно указать корректное значение https://confluence.mdtest.org/pages/viewpage.action?pageId=123014677
        /// </summary>
        public int ProductPartition { get; set; }

        /// <summary>
        /// Идентификатор почтовой рассылки (не воспринимает кириллицу)
        /// https://www.ietf.org/rfc/rfc2919.txt
        /// </summary>
        public string ListId { get; set; }

        /// <summary> 
        /// Обычная строка или json индивидуальные параметры каждого WL 
        /// </summary>
        public string WLSpecialParams { get; set; }

        /// <summary>
        /// MessageId письма, в ответ на которое отправляется текущее письмо
        /// https://datatracker.ietf.org/doc/html/rfc822#section-4.6.2
        /// </summary>
        public string InReplyTo { get; set; }

        /// <summary>
        /// Список MessageId писем, с которыми связано данное письмо (используется для отображения цепочки писем)
        /// https://datatracker.ietf.org/doc/html/rfc822#section-4.6.3
        /// </summary>
        public IList<string> References { get; set; }

        /// <summary>
        /// Уникальный идентификатор текущего письма
        /// https://datatracker.ietf.org/doc/html/rfc1036#section-2.1.5
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Отслеживать открытие письма. Работает только если IsBodyHtml = true и Body - HTML
        /// </summary>
        public bool TrackOpening { get; set; } = true;
    }
}
