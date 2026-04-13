namespace Moedelo.Money.Enums
{
    /// <summary>
    /// Тип платежа (КБК)
    /// </summary>
    public enum KbkPaymentType
    {
        /// <summary>
        /// Взносы
        /// </summary>
        Payment = 0,

        /// <summary>
        /// Пени
        /// </summary>
        Surcharge = 1,

        /// <summary>
        /// Штрафы
        /// </summary>
        Forfeit = 2
    }
}
