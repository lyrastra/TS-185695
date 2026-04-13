using System;
using System.Collections.Generic;

namespace Moedelo.Edm.Dto.TsWizard
{
    /// <summary>
    /// Письмо (только нужные нам данные)
    /// </summary>
    public class EmailContentDto
    {
        /// <summary>
        /// Тема
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело (HTML или текст в зависимости от письма)
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Дата, когда было получено письмо
        /// </summary>
        public DateTime GetAt { get; set; }

        /// <summary>
        /// Вложения
        /// </summary>
        public List<EmailAttachmentDto> Attachments { get; set; }

    }
}