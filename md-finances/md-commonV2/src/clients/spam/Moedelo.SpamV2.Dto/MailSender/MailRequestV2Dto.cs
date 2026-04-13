using System.Collections.Generic;

namespace Moedelo.SpamV2.Dto.MailSender
{
    public class MailRequestV2Dto
    {
        public string Login { get; set; }

        public string HostName { get; set; }

        /// <summary>
        /// Параметры письма в виде последовательного списка
        /// </summary>
        public List<string> Args { get; set; }

        /// <summary>
        /// Параметры письма в виде ассоциативного массива (именованные параметры)
        /// </summary>
        public Dictionary<string, string> NamedArgs { get; set; } = new Dictionary<string, string>();

        /// <summary>Шаблон письма </summary>
        public int Marker { get; set; }

        /// <summary>Разделение шаблонов писем по продуктам</summary>
        public int ProductPartition { get; set; }

        /// <summary> Отвечает за отображение превью письма, которое видно после темы</summary>
        public MailFirmDataRequestDto MailFirmData { get; set; }
    }
}
