namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Создание обращения
    /// </summary>
    public class CreateCaseByContactDto
    {
        /// <summary>
        ///     email часть обязательна
        /// </summary>
        public ContactDto Contact { get; set; }

        /// <summary>
        ///     не обязательно, но это назавание у первого сообщения у обращения, так что крайне желательна
        /// </summary>
        public string Subj { get; set; }

        /// <summary>
        ///     Текст сообщения - обязательно
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     тип обращения, обязательно
        /// </summary>
        public CaseTypeDto CaseType { get; set; }

        /// <summary>
        ///     источник, обязательно
        /// </summary>
        public CaseSource CaseSource { get; set; }

        /// <summary>
        ///     не обязательно, но если это продолжение кейса то наиболее желательно
        /// </summary>
        public int? CaseNumber { get; set; }

        /// <summary>
        ///     не обязательно, по умолчанию возьмет либо из обращения,
        ///     либо бизнес процесс расставит при создании обращения
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        ///     не обязательно, используется для забора из почты
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        ///     не обязательно, используется при создании обращения для принудительной привязки контрагента
        /// </summary>
        public string AccountId { get; set; }
    }
}