using System.ComponentModel;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy
{
    public enum BankruptTypeEnum
    {
        /// <summary>
        /// Все типы сообщений
        /// </summary>
        [Description("Все типы сообщений")]
        All = 0,

        /// <summary>
        /// Торги и инвентаризация
        /// </summary>
        [Description("Торги и инвентаризация")]
        BiddingAndInventory = 1,

        /// <summary>
        /// Судебные акты
        /// </summary>
        [Description("Судебные акты")]
        JudicialActs = 2,

        /// <summary>
        /// Собрания кредиторов
        /// </summary>
        [Description("Собрания кредиторов")]
        MeetingsOfCreditors = 3,

        /// <summary>
        /// Требования кредиторов
        /// </summary>
        [Description("Требования кредиторов")]
        CreditorsClaims  = 4,

        /// <summary>
        /// Сделки и ответственность
        /// </summary>
        [Description("Сделки и ответственность")]
        TransactionsAndLiability = 5,

        /// <summary>
        /// Все иные сообщения
        /// </summary>
        [Description("Все иные сообщения")]
        Other = 6
    }
}
