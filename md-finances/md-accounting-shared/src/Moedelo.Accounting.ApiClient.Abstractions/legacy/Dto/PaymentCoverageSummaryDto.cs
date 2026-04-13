namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
{
    public class PaymentCoverageSummaryDto
    {
        /// <summary>
        /// Индетификатор фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Количество полностью покрытых платежей
        /// </summary>
        public long FullyCoveredCount { get; set; }

        /// <summary>
        /// Количество частично покрытых платежей
        /// </summary>
        public long PartiallyCoveredCount { get; set; }

        /// <summary>
        /// Количество непокрытых платежей
        /// </summary>
        public long UncoveredCount { get; set; }
    }
}