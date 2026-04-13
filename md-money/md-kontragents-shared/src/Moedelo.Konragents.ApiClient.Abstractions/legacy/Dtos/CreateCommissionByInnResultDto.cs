namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public class CreateCommissionByInnResultDto
    {
        /// <summary>
        /// -1: нет прав
        /// -2: контрагент с таким ИНН уже существует
        /// 0: создан
        /// </summary>
        public int Status { get; set; }
        
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Идентификатор склада комиссионера
        /// </summary>
        public long CommissionAgentStockId { get; set; }

        /// <summary>
        /// Наименоване контрагента
        /// </summary>
        public string Name { get; set; }
    }
}