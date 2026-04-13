using System.ComponentModel;

namespace Moedelo.PayrollV2.Dto.ChargeSettings
{
    public enum SalaryPayType
    {
        [Description("за месяц")]
        Month = 0, // оплата за месяц
        
        [Description("за смену")]
        Shift = 1, // оплата за смену
        
        [Description("за час")]
        Hour = 2, // оплата за час
    }
}