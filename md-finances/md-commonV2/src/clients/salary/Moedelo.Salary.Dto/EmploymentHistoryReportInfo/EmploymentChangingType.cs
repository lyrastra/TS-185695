using System.ComponentModel;

namespace Moedelo.Salary.Dto.EmploymentHistoryReportInfo
{
    public enum EmploymentChangingType : byte
    {
        [Description("Прием")]
        Recruitment = 1,
        [Description("Перевод")]
        PositionChanging = 2,
        [Description("Увольнение")]
        Firing = 5
    }
}