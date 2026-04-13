namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public class KontragentDebtDto
    {
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public long KontragentId { get; set; }
    
        /// <summary>
        /// Сумма задолженности
        /// </summary>
        public decimal Debt { get; set; }
    }
}