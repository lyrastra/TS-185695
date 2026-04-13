using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    /// <summary>
    /// Группа инвалидности
    /// </summary>
    public enum DisabilityGroupType : byte
    {
        [Description("1 - первая группа")]
        Type1 = 1,

        [Description("2 - вторая группа")]
        Type2 = 2,

        [Description("3 - третья группа")]
        Type3 = 3,
        
        [Description("Установлена утрата проф. трудоспособности")]
        LossProfessionalAbilityToWork = 9,
        
        [Description("Установлена утрата проф. трудоспособности")]
        LossProfessionalAbilityToWorkV2 = 29
    }
}