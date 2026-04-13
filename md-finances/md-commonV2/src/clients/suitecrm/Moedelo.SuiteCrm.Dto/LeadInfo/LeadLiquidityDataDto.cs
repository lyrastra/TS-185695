namespace Moedelo.SuiteCrm.Dto.LeadInfo
{
    public class LeadLiquidityDataDto
    {
        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Конвертирован в контрагента
        /// </summary>
        public bool IsConverted { get; set; }

        /// <summary>
        /// Информирован банком
        /// </summary>
        public string InformedByBankWorker { get; set; }

        /// <summary>
        /// Причина отказа
        /// </summary>
        public string DeclineCase { get; set; }

        /// <summary>
        /// Статус лида
        /// </summary>
        public string Status { get; set; }
    }
}