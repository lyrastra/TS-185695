using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public enum ChildCareLivingConditionsType
    {
        [Description("Проживает")]
        Living = 1,
        [Description("Работает")]
        Working = 2
    }
}