namespace Moedelo.AccountingV2.Dto.MoneyStatistics
{
    public class FirmIncomeDto
    {
        /// <summary>
        /// Id фирмы
        /// </summary>
        public int FirmId { get; set; }
        /// <summary>
        /// Сумма дохода
        /// </summary>
        public decimal Income { get; set; }
    }
}