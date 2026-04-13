namespace Moedelo.Common.Enums.Enums.Billing
{
    /// <summary>
    /// Тип метода оплаты
    /// </summary>
    public enum PaymentMethodType
    {
        // р/сч 
        SettlementAccount = 1,
        // электронные деньги
        EMoney = 2,
        // технический
        Technical = 3,
        // бесплатный
        Free = 4,
        // white label
        WhiteLabel = 5
    }
}