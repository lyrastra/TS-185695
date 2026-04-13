using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    /// <summary>
    /// Статус нетрудоспособного
    /// </summary>
    public enum DisabledStatus : byte
    {
        [Description("31 - продолжает болеть")]
        Status31 = 31,

        [Description("32 - установлена инвалидность")]
        Status32 = 32,

        [Description("33 - изменена группа инвалидности")]
        Status33 = 33,

        [Description("34 - умер")]
        Status34 = 34,

        [Description("35 - отказ от проведения медико-социальной экспертизы")]
        Status35 = 35,

        [Description("36 - явился трудоспособным")] 
        Status36 = 36,
        
        [Description("37 - долечивание")] 
        Status37 = 37
    }
}