using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Evotor
{
    public enum RetailRefundStatus
    {
        [Description("Был создан, но удалён пользователем")]
        RemovedByUser = -1,

        [Description("Возврат наличной оплатой успешно создан")]
        CreatedCashPayment = 0,

        [Description("Возврат безналичной оплатой успешно создан")]
        CreatedCashlessPayment = 1,

        [Description("Возврат не создан, т.к. содержит позиции товаров, проданные по свободной продаже")]
        FreeSalePositions = 2,

        [Description("Возврат не создан, т.к. для него не удалось найти чек в Облаке Эвотора")]
        NoSellFound = 3,

        [Description("Возврат не создан, т.к. не удалось найти ОРП для смены, указанной в возврате")]
        NoRetailReport = 4,

        [Description("Возврат не создан, т.к. отсутствует один или несколько товаров в ОРП, на которое ссылается возврат")]
        NoItems = 5,

        [Description("Возврат не создан, т.к. ОРП, на которое ссылается возврат, находится в закрытом периоде")]
        RetailReportIsInClosedPeriod = 6,

        [Description("Возврат не создан, т.к. принадлежит пользователю системы БИЗ")]
        BizUser = 7,

        [Description("Возврат не создан, т.к. возвраты не реализованы для пользователей ОСНО")]
        OsnoUser = 8,

        [Description("Возврат не создан, т.к. раздел \"Запасы\" для данного пользователя отключён")]
        StockIsDisabled = 10,

        [Description("Возврат не создан, т.к. возникло необработанное исключение при его создании")]
        PaybackProcessionUnhandledException = 66
    }
}
