namespace Moedelo.TaxPostings.Kafka.Abstractions.Commands
{
    public enum PostingsCommandType
    {
        /// <summary>
        /// Перезаписать проводки
        /// </summary>
        Overwrite = 1,

        /// <summary>
        /// Удалить проводки
        /// </summary>
        Delete = 2
    }
}
