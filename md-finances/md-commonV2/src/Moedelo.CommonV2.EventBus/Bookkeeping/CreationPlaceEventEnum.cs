namespace Moedelo.CommonV2.EventBus.Bookkeeping
{
    // Копия энама CreationPlace из md-docs-shared.
    // Копия создана, чтобы не тащить в md-commonV2 сабмодуль md-docs-shared.
    // Используется при синхронизации счетов-фактур с md-docsNetCore
    public enum CreationPlaceEventEnum
    {
        Default = 0,

        /// <summary>
        /// Из остатков
        /// </summary>
        Balances = 1,

        /// <summary>
        /// Из имущества
        /// </summary>
        Estate = 2,

        /// <summary>
        /// Из накладной
        /// </summary>
        Waybill = 3,

        /// <summary>
        /// Из Акта
        /// </summary>
        Statement = 4
    }
}