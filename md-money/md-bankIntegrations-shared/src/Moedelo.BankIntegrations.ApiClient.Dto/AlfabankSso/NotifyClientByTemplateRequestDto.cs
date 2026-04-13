using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Dto.AlfabankSso
{
    public class NotifyClientByTemplateRequestDto
    {
        public int FirmId { get; set; }

        /// <summary> Идентификатор шаблона сообщения </summary>
        public string TemplateId { get; set; }

        /// <summary> Параметры, которые подставляются в шаблон </summary>
        public Dictionary<string, string> Params { get; set; }
    }
}
