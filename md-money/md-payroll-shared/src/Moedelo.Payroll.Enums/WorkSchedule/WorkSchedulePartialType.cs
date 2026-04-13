using System.ComponentModel;

namespace Moedelo.Payroll.Shared.Enums.WorkSchedule
{
    public enum WorkSchedulePartialType
    {
        [Description("Неполный рабочий день")]
        PartTimeWorkDay = 0,
        [Description("Неполная рабочая неделя")]
        PartTimeWorkWeek = 1
    }
}