namespace Moedelo.ReceiptStatement.Enums
{
    public enum NdsType
    {
        UnknownNds = 0,

        /// <summary>
        /// Без НДС
        /// </summary>
        WithoutNds = 1,

        /// <summary>
        /// НДС 0 %
        /// </summary>
        Nds0 = 2,

        /// <summary>
        /// НДС 10 %
        /// </summary>
        Nds10 = 3,

        /// <summary>
        /// НДС 18 %
        /// </summary>
        Nds18 = 4,

        /// <summary>
        /// НДС 20 %
        /// </summary>
        Nds20 = 5
    }
}
