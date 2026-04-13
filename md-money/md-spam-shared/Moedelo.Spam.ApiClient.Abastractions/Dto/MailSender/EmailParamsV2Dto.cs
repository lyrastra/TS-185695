using System.Collections.Generic;
using Moedelo.Spam.ApiClient.Abastractions.Enums.Common;
using Moedelo.Spam.ApiClient.Abastractions.Enums.MailSender;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.MailSender
{
    public class EmailParamsV2Dto
    {
        public string Login { get; set; }

        public string HostName { get; set; }

        /// <summary>
        /// Параметры письма в виде ассоциативного массива (именованные параметры, в теле письма на них ссылаются токены вида {nameOfArg})
        /// </summary>
        public Dictionary<string, string> NamedArgs { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Параметры письма в виде последовательного списка (неименованные параметры, в теле письма на них ссылаются токены вида {0})
        /// </summary>
        public List<string> ArgsList { get; set; } = new List<string>();

        /// <summary> Идентификатор шаблон письма </summary>
        public UnionEmailMarker Marker { get; set; }

        /// <summary> Разделение шаблонов писем по продуктам </summary>
        public WLProductPartition ProductPartition { get; set; }

        /// <summary> Отвечает за отображение превью письма, которое видно после темы </summary>
        public MailFirmDataRequestDto MailFirmData { get; set; }

        public string FromAddress { get; set; }

        public string FromName { get; set; }

        public string Subject { get; set; }

        public List<string> Addresses { get; set; } = new List<string>();

        public List<string> ReplyToAdresses { get; set; } = new List<string>();

        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();

        public string LabelForIdentification { get; set; }

        /// <summary>Идентификатор почтовой рассылки(не воспринимает кириллицу): https://yandex.ru/support/postoffice/advices.html</summary>
        public string ListId { get; set; }

        /// <summary> обычная строка или json индивидуальные параметры каждого WL </summary>
        public string WLSpecialParams { get; set; }

        /// <summary>
        /// Отслеживать открытие письма
        /// </summary>
        public bool TrackOpening { get; set; } = true;
    }
}