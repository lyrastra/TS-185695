namespace Moedelo.BPM.CaseDocs.Client.Dtos
{
    /// <summary>
    ///     Результат создания обращения
    /// </summary>
    public class CreateCaseResultDto
    {
        /// <summary>
        ///     Идентификатор обращения
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Номер обращения
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///     Идентификатор сообщения
        /// </summary>
        public string MessageId { get; set; }
    }
}