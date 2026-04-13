namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.RetailReports.Dto
{
    public class RetailReportReasonRevenueDto
    {
        /// <summary>
        /// Идентификатор кассы
        /// </summary>
        public long CashierId { get; set; }

        /// <summary>
        /// Идентификатор платёжного поручения
        /// </summary>
        public long RetailRevenueId { get; set; }
    }
}