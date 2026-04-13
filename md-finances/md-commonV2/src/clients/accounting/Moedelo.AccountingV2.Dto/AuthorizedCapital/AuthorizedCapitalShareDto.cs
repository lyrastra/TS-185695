namespace Moedelo.AccountingV2.Dto.AuthorizedCapital
{
    /// <summary>
    /// Доля контрагента в УК
    /// </summary>
    public class AuthorizedCapitalShareDto
    {
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Название контрагента
        /// </summary>
        public string KontragentName { get; set; }

        /// <summary>
        /// Сумма взноса (доля) в УК
        /// </summary>
        public decimal Sum { get; set; }
    }
}