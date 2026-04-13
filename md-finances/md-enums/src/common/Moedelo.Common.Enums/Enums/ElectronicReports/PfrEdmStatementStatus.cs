using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum PfrEdmStatementStatus
    {
        [Description("Не отправлялось")]
        NotSent = 0,
        [Description("Ожидание")]
        InProgress,
        [Description("Принято вручную")]
        DoneManually,
        [Description("Принято в ПФР")]
        Done,
        [Description("Отклонено")]
        Failed,
        [Description("Требуется повторная отправка")]
        RequiredResending,
        [Description("Не подписано")]
        NotSigned,
    }
}