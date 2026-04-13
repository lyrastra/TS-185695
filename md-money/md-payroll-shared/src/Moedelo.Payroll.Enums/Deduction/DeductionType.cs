using System.ComponentModel;

namespace Moedelo.Payroll.Shared.Enums.Deduction;

public enum DeductionType : byte
{
    [Description("Алименты")]
    Aliment = 0,

    [Description("Прочие удержания")]
    Other = 1
}