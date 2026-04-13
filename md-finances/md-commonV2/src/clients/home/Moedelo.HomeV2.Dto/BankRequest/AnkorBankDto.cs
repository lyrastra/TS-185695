namespace Moedelo.HomeV2.Dto.BankRequest
{
    public class AnkorBankDto
    {
        /// <summary>
        /// Направление деятельности
        /// </summary>
        public string BusinessProfile { get; set; }

        /// <summary>
        /// Среднемесячный оборот
        /// </summary>
        public string AverageMonthlyTurnover { get; set; }

        /// <summary>
        /// Наличие эквайринга
        /// </summary>
        public bool HasAcquiring { get; set; }

        /// <summary>
        /// Желаемый размер аванса
        /// </summary>
        public string DesiredPrepayment { get; set; }

        /// <summary>
        /// Планируемый срок погашения
        /// </summary>
        public string ExpectedMaturity { get; set; }
    }
}
