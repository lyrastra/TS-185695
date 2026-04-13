
namespace Moedelo.AccountingV2.Client.MediationContractRevenue.Dto

{
    public class MediationContractRevenueDto
    {
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата 
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Id кассы
        /// </summary>
        public long CashId { get; set; }

        /// <summary>
        /// Приложение
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Основание
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// В том числе оплаченно картой
        /// </summary>
        public decimal? PaidCardSum { get; set; }

        /// <summary>
        /// Мое вознаграждение
        /// </summary>
        public decimal? MyReward { get; set; }

        /// <summary>
        /// Договор посредничества
        /// </summary>
        public MiddlemanContractClientDataDto MiddlemanContract { get; set; }

        /// <summary>
        /// Id кассира
        /// </summary>
        public int? WorkerId { get; set; }

        /// <summary>
        /// Имя кассира
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        /// DocumentBaseId созданного документа
        /// </summary>
        public long? BaseDocumentId { get; set; }

        /// <summary>
        /// id созданного документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер Z отчёта
        /// </summary>
        public string ZReportNumber { get; set; }
    }
}
