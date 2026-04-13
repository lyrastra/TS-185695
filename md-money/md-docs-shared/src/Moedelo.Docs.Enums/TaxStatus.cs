namespace Moedelo.Docs.Enums
{
    public enum TaxStatus
    {
        /// <summary>
        /// Не облагается по законодательству
        /// </summary>
        NotTax = 0,

        /// <summary>
        /// Вручную
        /// </summary>
        ByHand = 1,

        /// <summary>
        /// Облагается, есть проводки
        /// </summary>
        Yes = 2,

        /// <summary>
        /// Облагается, нет проводок
        /// </summary>
        No = 3,

        /// <summary>
        /// По связанному документу
        /// </summary>
        ByLinkedDocument = 4
    }
}