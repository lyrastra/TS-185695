using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances
{
    public enum RadiationReasonType
    {
        [Description("ЧАЭС")]
        Chernobyl = 1,
        [Description("Семипалатинск")]
        Semipalatinsk = 2,
        [Description("Маяк")]
        Mayak = 3,
        [Description("Граждане из подразделений особого риска")]
        HighRiskCitizens = 4
    }
}