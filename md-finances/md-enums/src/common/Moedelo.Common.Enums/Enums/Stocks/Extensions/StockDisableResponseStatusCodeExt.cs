namespace Moedelo.Common.Enums.Enums.Stocks.Extensions
{
    public static class StockDisableResponseStatusCodeExt
    {
        public static string GetMessage(this StockDisableResponseStatusCode status)
        {
            switch (status)
            {
                case StockDisableResponseStatusCode.AdvanceStatementReferencesExists:
                    return "Авансовые отчёты связаны с документами из раздела Покупки, Товарами и Материалами из раздела Запасы, удалите такие авансовые отчёты для отключения модуля Покупки и Запасы.";
                case StockDisableResponseStatusCode.SaleReferencesExists:
                    return "Накладные, акты из раздела Покупки привязаны к Списаниям. Удалите такие документы в разделе Деньги - Списания.";
                default:
                    return string.Empty;
            }
        }
    }
}
