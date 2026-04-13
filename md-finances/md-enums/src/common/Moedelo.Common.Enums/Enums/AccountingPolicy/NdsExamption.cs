namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Применение освобождения от НДС в отношении операций, перечисленных в п. 3 ст. 149 НК РФ.
    /// </summary>
    public enum NdsExamption
    {
        /// <summary>
        /// Применяем освобождение от НДС на основании п.3 ст.149 НК РФ.
        /// </summary>
        OnBasisOfParagraph3OfArticle149OfTaxCode = 1,

        /// <summary>
        /// Не пользуюсь налоговыми льготами по НДС.
        /// </summary>
        DoNotUseTaxBreakForNds = 2
    }
}