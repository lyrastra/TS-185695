using System;


namespace Moedelo.AccountingV2.Client.TransferRetailRevenue.Dto
{
    public class AccountingCashOrderDto
    {
        /// <summary>
        /// Id Кассы
        /// </summary>
        public long CashId { get; set; }
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        public bool isOOO { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        public int WorkerId { get; set; }

        public string WorkerName { get; set; }

        public string FirmName { get; set; }

        public decimal? PaidCardSum { get; set; }

        /// <summary>
        /// Сумма для УСН
        /// </summary>
        public decimal? UsnSum { get; set; }

        /// <summary>
        /// Сумма ЕНВД
        /// </summary>
        public decimal? EnvdSum { get; set; }

        /// <summary>
        /// Сумма ПСН
        /// </summary>
        public decimal? PSNSum { get; set; }

        /// <summary>
        /// Номер Z-отчета или БСО 
        /// </summary>
        public string Number { get; set; }


        /// <summary>
        /// Сумма по Z-отчёту
        /// </summary>
        public decimal Zsum { get; set; }

        /// <summary>
        /// Описание Z-отчет (БСО) № 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// например только Розничная выручка
        /// </summary>
        public string Destanation { get; set; }

        /// <summary>
        /// Привязанные патенты
        /// </summary>
        public long? PatentId { get; set; }
    }
}
