namespace Moedelo.BizV2.Dto.TaxSystemSum
{
    public class TaxSystemSumDto
    {
        /// <summary>
        /// Сумма доходов
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// Сумма расходов
        /// </summary>
        public decimal Expense { get; set; }

        /// <summary>
        /// Тип СНО
        /// </summary>
        public TaxSystemType TaxSystemType { get; set; }

        /// <summary>
        /// указанная СНО в TaxSystemType явялется основной
        /// </summary>
        public bool IsMainTaxSystem { get; set; }
    }
}
