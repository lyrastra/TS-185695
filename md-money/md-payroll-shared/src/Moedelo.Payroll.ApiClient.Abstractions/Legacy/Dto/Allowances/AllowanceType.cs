using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances
{
    public enum AllowanceType
    {
        [Description("Нетрудоспособность")]
        SickList = 1,
        [Description("Беременность и роды")]
        PregnancyAndChildbirth = 2,
        [Description("Ранние сроки беременности")]
        EarlyPregnancy = 3,
        [Description("Рождение ребенка")]
        ChildBirth = 4,
        [Description("Уход за ребенком")]
        ChildCare = 5
    }
}