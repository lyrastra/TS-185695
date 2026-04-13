namespace Moedelo.Common.Enums.Enums.Contract
{
    public enum MediationType
    {
        /// <summary>
        /// Я посредник, продаю услуги от своего имени и участвую в расчетах
        /// </summary>
        MiddlemanSellServicesOnMyBehalfAndParticipateCalculations = 1,

        /// <summary>
        /// Я посредник, закупаю товары/услуги от имени заказчика и не участвую в расчетах
        /// </summary>
        MiddlemanPurchaseGoodsAndServicesBehalfCustomer = 2,

        /// <summary>
        /// Я посредник, продаю услуги от имени заказчика
        /// </summary>
        MiddlemanSellServicesBehalfCustomer = 3,

        /// <summary>
        /// Я посредник, продаю товары от своего имени
        /// </summary>
        MiddlemanSellGoodsOnMyBehalf = 4,

        /// <summary>
        /// Я посредник, продаю товары от имени заказчика
        /// </summary>
        MiddlemanSellGoodsBehalfCustomer = 5,

        /// <summary>
        /// Я посредник, закупаю услуги от своего имени
        /// </summary>
        MiddlemanBuyServicesOnMyBehalf = 6,

        /// <summary>
        /// Я посредник, закупаю услуги от имени заказчика
        /// </summary>
        MiddlemanBuyServicesBehalfCustomer = 7,

        /// <summary>
        /// Я посредник, закупаю товары от своего имени
        /// </summary>
        MiddlemanBuyGoodsOnMyBehalf = 8,

        /// <summary>
        /// Я посредник, закупаю товары от имени заказчика
        /// </summary>
        MiddlemanBuyGoodsBehalfCustomer = 9,

        /// <summary>
        /// Я заказчик (принципал), продаю услуги через посредника
        /// </summary>
        CustomerSellServicesThroughMiddleman = 10,

        /// <summary>
        /// Я заказчик (принципал), продаю товары через посредник
        /// </summary>
        CustomerSellGoodsThroughMiddleman = 11,

        /// <summary>
        /// Я заказчик (принципал), закупаю товары/услуги через посредника
        /// </summary>
        CustomerSellGoodsAndServicesThroughMiddleman = 12
    }
}
