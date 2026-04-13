using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Evotor.Sessions
{
    public enum SortType
    {
        [Description("По дате открытия смены (по возрастанию)")]
        ByOpeningDateAsc = 0,

        [Description("По дате открытия смены (по убыванию)")]
        ByOpeningDateDesc = 1,

        [Description("По дате закрытия смены (по возрастанию)")]
        ByClosingDateAsc = 2,

        [Description("По дате закрытия смены (по убыванию)")]
        ByClosingDateDesc = 3,

        [Description("По имени магазина (по возрастанию)")]
        ByStoreNameAsc = 4,

        [Description("По имени магазина (по убыванию)")]
        ByStoreNameDesc = 5,

        [Description("По имени кассы (по возрастанию)")]
        ByDeviceNameAsc = 6,

        [Description("По имени кассы (по убыванию)")]
        ByDeviceNameDesc = 7,

        [Description("По сумме Z-отчёта (по возрастанию)")]
        ByZReportSumAsc = 8,

        [Description("По сумме Z-отчёта (по убыванию)")]
        ByZReportSumDesc = 9
    }
}