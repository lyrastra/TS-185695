namespace Moedelo.Accounts.Kafka.Abstractions.Events
{
    /// <summary>
    /// Событие "Объединение аккаунтов: другой аккаунт был присоединён к этому аккаунту"
    /// </summary>
    public abstract class AccountWasMergedWithAnotherAccountEventFields
    {
        /// <summary>
        /// идентификатор аккаунта
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// идентификатор основной фирмы аккаунта
        /// </summary>
        public int AccountMainFirmId { get; set; }
        
        /// <summary>
        /// идентификатор аккаунта, который был присоединён к текущему
        /// внимание: этот аккаунт удаляется в процессе присоединения, поэтому получить данные по нему не получится
        /// </summary>
        public int MergedAccountId { get; set; }

        /// <summary>
        /// главная фирма основного пользователя аккаунта, который был присоединён к текущему аккаунту
        /// </summary>
        public int MergedAccountMainFirmId { get; set; }
    }
}