using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Evotor
{
    public enum RetailReportStatus
    {
        [Description("Был создан, но удалён пользователем")]
        RemovedByUser = -1,

        [Description("ОРП успешно создан")]
        SuccessfullySaved = 0,

        [Description("ОРП не создан, т.к. его дата находится в закрытом периоде")]
        IsInClosedPeriod = 1,

        [Description("ОРП не создан, т.к. в чеках содержатся невалидные позиции(либо только свободные продажи, либо только услуги)")]
        NoValidPositions = 2,

        [Description("ОРП не создан, т.к. в смене нет чеков")]
        NoSells = 3,

        [Description("ОРП не создан, т.к. ранее был не создан Z-Отчёт")]
        NoZReport = 4,

        [Description("ОРП не создан, т.к. в чеках содержатся только позиции, проданные как товары, но указанные как услуги в облаке Эвотор")]
        OnlyChangedTypePositions = 5,

        [Description("ОРП не создан, т.к. раздел \"Запасы\" для данного пользователя отключён")]
        StockIsDisabled = 10,
        
        [Description("ОРП не был создан, т.к. раздел \"Запасы\" для данного пользователя был отключён в старом личном кабинете")]
        StockWasDisabled = 11,

        [Description("ОРП не создан, т.к. во время его обработки произошло непредвиденное исключение")]
        UnhandledException = 66
    }
}
