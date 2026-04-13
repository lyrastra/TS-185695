using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moedelo.Common.Enums.Enums.Email;
using Moedelo.Common.Enums.Enums.Products;

namespace Moedelo.SpamV2.Dto.MailSender
{
    public class EmailParamsV2Dto
    {
        /// <summary>
        /// Логин пользователя, под которого формируется письмо (брендирование, проверка согласия, ссылки на отписку)
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }

        /// <summary>
        /// Идентификатор фирмы, в контексте которой формируется письмо
        /// Используется для проверки корректности переданного ProductPartition
        /// </summary>
        public int? FirmId { get; set; }

        public string HostName { get; set; }

        /// <summary>
        /// Параметры письма в виде ассоциативного массива (именованные параметры, в теле письма на них ссылаются токены вида {nameOfArg})
        /// </summary>
        public Dictionary<string, string> NamedArgs { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Параметры письма в виде последовательного списка (неименованные параметры, в теле письма на них ссылаются токены вида {0})
        /// </summary>
        public List<string> ArgsList { get; set; } = new List<string>();

        /// <summary> 
        /// Идентификатор шаблон письма 
        /// </summary>
        public UnionEmailMarker Marker { get; set; }

        /// <summary>
        /// Продукт клиента
        /// Используется для брендирования, можно получить из IProductPartitionApiClient
        /// Важно указать корректное значение https://confluence.mdtest.org/pages/viewpage.action?pageId=123014677
        /// </summary>
        public WLProductPartition ProductPartition { get; set; }

        /// <summary> 
        /// Отвечает за отображение превью письма, которое видно после темы 
        /// </summary>
        public MailFirmDataRequestDto MailFirmData { get; set; }

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
        /// Получатели
        /// </summary>
        [Required]
        public List<string> Addresses { get; set; } = new List<string>();

        /// <summary>
        /// Кому отправлять ответы на данное письмо
        /// </summary>
        public List<string> ReplyToAdresses { get; set; } = new List<string>();

        /// <summary>
        /// Вложения
        /// </summary>
        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();

        /// <summary>
        /// Идентификатор письма на стороне отправителя (можно использовать для сопоставления с результатом отправки)
        /// </summary>
        public string LabelForIdentification { get; set; }

        /// <summary>
        /// Идентификатор почтовой рассылки(не воспринимает кириллицу)
        /// https://www.ietf.org/rfc/rfc2919.txt
        /// </summary>
        public string ListId { get; set; }

        /// <summary> 
        /// Обычная строка или json индивидуальные параметры каждого WL 
        /// </summary>
        public string WLSpecialParams { get; set; }

        /// <summary>
        /// Отслеживать открытие письма
        /// </summary>
        public bool TrackOpening { get; set; } = true;
    }
}