namespace Moedelo.Money.Enums
{
    public enum MoneySourceType
    {
        All = 0,

        /// <summary>
        /// Расчетный счет
        /// </summary>
        SettlementAccount = 1,

        /// <summary>
        /// Касса
        /// </summary>
        Cashbox = 2,

        /// <summary>
        /// Эл. кошелек
        /// </summary>
        Purse = 3
    }
}
