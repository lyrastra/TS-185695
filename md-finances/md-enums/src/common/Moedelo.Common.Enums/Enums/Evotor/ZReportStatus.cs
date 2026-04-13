using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Evotor
{
    public enum ZReportStatus
    {
        [Description("Был создан, но удалён пользователем")]
        RemovedByUser = -1,

        [Description("Z-отчёт успешно создан")]
        SuccessfullySaved = 0,

        [Description("Z-отчёт не найден в Эвоторе. Возможно, смена ещё не закрыта")]
        NotFound = 1,

        [Description("Z-отчёт не создан, т.к. из ответа Эвотора не удалось получить необходимые для его создания данные")]
        InvalidResponseModel = 2,

        [Description("Z-отчёт не создан, т.к. его дата находится в закрытом периоде")]
        IsInClosedPeriod = 3,

        [Description("Z-отчёт не создан, т.к. для текущей смены найдено больше чем один Z-отчётов")]
        MoreThanOneFound = 4,

        [Description("Z-отчёт не создан, т.к. кассовый аппарат для текущей смены отсутствует в Эвоторе, вероятно он был удалён")]
        DeletedCashbox = 5,

        [Description("Z-отчёт не создан, т.к. в смене нет чеков")]
        NoSells = 6,

        [Description("Z-отчёт не создан, т.к. во время его обработки произошло исключение типа TaskCanceledException")]
        TaskCanceledException = 65,

        [Description("Z-отчёт не создан, т.к. во время его обработки произошло непредвиденное исключение")]
        UnhandledException = 66
    }
}
