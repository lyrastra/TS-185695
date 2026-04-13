using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkerEmploymentChanges
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