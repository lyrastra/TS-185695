namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Срок полезного использования основных средств.
    /// </summary>
    public enum UsefulLifeOfFixedAssets
    {
        /// <summary>
        /// Исходя из нормативно-правовых и других ограничений 
        /// с учетом Классификации ОС
        /// </summary>
        ByLegalRestrictionsOnUseOfFixedAsset = 1,

        /// <summary>
        /// Исходя из ожидаемого срока использования объекта 
        /// в соответствии с ожидаемой производительностью или мощностью.
        /// </summary>
        ByPeriodOfUseOfFixedAsset = 2,

        /// <summary>
        /// Исходя из ожидаемого физического износа,
        /// зависящего от режима эксплуатации (количества смен), естественных
        /// условий и влияния агрессивной среды, системы проведения ремонта.
        /// </summary>
        ByExpectedPhysicalWear  = 3
    }
}