namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Создание обращения
    /// </summary>
    public class CreateCaseByUserAndFirmDto
    {
        /// <summary>
        ///     Идентификатор пользователя из МД, обязательно
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Идентификатор фирмы из МД, обязательно
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        ///     Тема\заголовок обращения
        ///     не обязательно
        ///     TODO: проверить утверждение: но это назавание у первого сообщения у обращения, так что крайне желательна для
        ///     первого сообщения
        /// </summary>
        public string Subj { get; set; }

        /// <summary>
        ///     Описание обращения
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Текст сообщения - обязательно
        /// </summary>
        public string Text { get; set; }

        /// TODO: переделать на enum
        /// <summary>
        ///     Тип обращения, обязательно
        /// </summary>
        public CaseTypeDto CaseType { get; set; }

        /// <summary>
        ///     Источник обращения, обязательно
        /// </summary>
        public CaseSource CaseSource { get; set; }

        /// <summary>
        ///     Номер обращения, в которое надо добавить новое сообщение.
        ///     Если не указан, то создастся новое обращение
        /// </summary>
        public int? CaseNumber { get; set; }
    }
}