using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Integration
{
    public enum BankStatementRequestStatus
    {
        [Description("Нового запроса нет")]
        NoBankRequest = 1,

        [Description("Ожидается ответ от банка")]
        BankRequestPending = 2,

        [Description("Запрос от банка получен, но выписка ещё не импортирована")]
        BankRequestComplete = 3
    }
}