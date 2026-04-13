namespace Moedelo.Docs.Enums
{
    // sync with: https://github.com/moedelo/md-enums/blob/749011e659f4628c3b44e83129d0bc246e3eb27a/src/common/Moedelo.Common.Enums/Enums/Documents/AdvanceStatementType.cs#L3 
    public enum AdvanceStatementType
    {
        /// <summary>
        /// Товары, материалы, услуги
        /// </summary>
        GoodsAndServices = 1,
        
        /// <summary>
        /// Оплата поставщику
        /// </summary>
        PaymentToSupplier = 2,
        
        /// <summary>
        /// Командировка
        /// </summary>
        Trip = 3
    }
}