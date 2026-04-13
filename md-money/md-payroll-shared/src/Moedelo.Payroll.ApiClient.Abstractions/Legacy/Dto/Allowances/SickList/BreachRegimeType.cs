using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    /// <summary>
    /// Тип нарушения режима
    /// </summary>
    public enum BreachRegimeType : byte
    {
        [Description("23 - несоблюдение предписанных условий оказания медицинской помощи")]
        Type23 = 23,

        [Description("24 - несвоевременная явка на прием к врачу (фельдшеру, зубному врачу)")]
        Type24 = 24,

        [Description("25 - выход на работу без выписки")]
        Type25 = 25,

        [Description("26 - отказ от направления в учреждение медико-социальной экспертизы")]
        Type26 = 26,

        [Description("27 - несвоевременная явка в учреждение медико-социальной экспертизы")]
        Type27 = 27,

        [Description("28 - другие нарушения")] 
        Type28 = 28
    }
}