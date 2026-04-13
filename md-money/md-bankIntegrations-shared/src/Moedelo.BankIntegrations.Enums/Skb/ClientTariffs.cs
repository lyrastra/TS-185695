namespace Moedelo.BankIntegrations.Enums.Skb
{
    public enum ClientTariffs
    {
        /// <summary> отсутствует значение, можно считать ошибкой </summary>
        NotInit = 0,

        /// <summary> Без сотрудников на год </summary>
        WithoutWorkers = 1,

        /// <summary> До 5 сотрудников на год </summary>
        WithFiveWorkers = 2,

        /// <summary> Максимальный на год </summary>
        Max = 3,

        /// <summary> Отчётность </summary>
        Reports = 4,

        /// <summary> Без сотрудников на 1 месяц </summary>
        WithoutWorkersMonth = 5,

        /// <summary> До 5 сотрудников на 1 месяц </summary>
        WithFiveWorkersMonth = 6,

        /// <summary> Максимальный на 1 месяц </summary>
        MaxMonth = 7,
    }
}