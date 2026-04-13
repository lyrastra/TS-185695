namespace Moedelo.Docs.Dto.SalesUpd.Rest
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