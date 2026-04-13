namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto
{
    public class SalesUpdNdsDeductionRestDto
    {
        /// <summary>
        /// Идентификатор авансового сч-ф
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Сумма вычета по авансовому сч-ф
        /// </summary>
        public decimal Sum { get; set; }
    }
}