using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    /// <summary>
    /// Родственная связь
    /// </summary>
    public enum RelationshipType : byte
    {
        [Description("38 - мать (мачеха)")]
        Type38 = 38,

        [Description("39 - отец (отчим)")]
        Type39 = 39,

        [Description("40 - опекун")]
        Type40 = 40,

        [Description("41 - попечитель")]
        Type41 = 41,

        [Description("42 - иной родственник, фактически осуществляющий уход за ребенком")]
        Type42 = 42
    }
}