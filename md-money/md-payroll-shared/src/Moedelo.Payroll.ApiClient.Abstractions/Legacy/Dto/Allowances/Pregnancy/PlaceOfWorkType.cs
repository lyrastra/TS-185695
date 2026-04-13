using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy
{
    /// <summary>
    /// Место работы
    /// </summary>
    public enum PlaceOfWorkType : byte
    {
        [Description("Основное")]
        General,
        [Description("По совместительству")]
        PartTimeJob
    }
}