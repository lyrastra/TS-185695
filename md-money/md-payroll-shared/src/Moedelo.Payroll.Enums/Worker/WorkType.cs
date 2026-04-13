using System.ComponentModel;

namespace Moedelo.Payroll.Shared.Enums.Worker
{
    public enum WorkType
    {
        [Description("Нет")]
        None = -1,

        [Description("Удаленная работа")]
        Remote = 0,

        [Description("Работа на дому")]
        FromHome = 1,
    }
}