namespace Moedelo.Common.Enums.Enums.Documents
{
    public enum WayBillTypesEnum
    {
        /// <summary> Покупка </summary>
        Buy = 100,

        /// <summary> Безвозмездная передача </summary>
        BuyDonation = 101,

        /// <summary> Поступление без документов </summary>
        BuyWithoutDocuments = 102,

        /// <summary> Возврат поставщику </summary>
        ReturnToSupplier = 103,

        /// <summary> Поступление агентских товаров на реализацию  </summary>
        AgentIncoming = 104,

        /// <summary> Продажа </summary>
        Sale = 200,

        /// <summary> Безвозмездная передача </summary>
        SaleDonation = 201,

        /// <summary> Возврат от покупателя </summary>
        ReturnFromClient = 202
    }
}