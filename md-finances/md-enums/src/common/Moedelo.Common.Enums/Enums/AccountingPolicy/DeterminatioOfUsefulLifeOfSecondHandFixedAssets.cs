namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Определение срока полезного использования основных средств, бывших в употреблении.
    /// </summary>
    public enum DeterminatioOfUsefulLifeOfSecondHandFixedAssets
    {
        /// <summary>
        /// С учетом срока эксплуатации предыдущим собственником в месяцах.
        /// </summary>
        WithPreviousOwnerUsefulLifeInMonth = 1,

        /// <summary>
        /// Норма амортизации определяется без учета срока полезного использования, 
        /// установленного предыдущим собственником.
        /// </summary>
        WithoutPreviousOwnerUsefulLife = 2
    }
}