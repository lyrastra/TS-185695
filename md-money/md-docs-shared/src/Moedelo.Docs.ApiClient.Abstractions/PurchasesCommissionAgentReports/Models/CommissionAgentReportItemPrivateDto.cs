namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models
{
    public class CommissionAgentReportItemPrivateDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Кол-во
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// При возврате: идентификатор отчета комиссионера, в котором была продажа 
        /// </summary>
        public long? RefundCommissionAgentReportId { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }
    }
}