using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Money
{
    public enum PaymentSources
    {
        [Description("Расчетный счет")]
        SettlementAccount = 1,
        [Description("Касса")]
        Cashbox = 2,
        [Description("Платежные системы")]
        Purse = 3
    }
}