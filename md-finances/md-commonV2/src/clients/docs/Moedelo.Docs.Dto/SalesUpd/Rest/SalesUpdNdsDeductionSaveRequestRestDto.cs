namespace Moedelo.Docs.Dto.SalesUpd.Rest
{
    public class SalesUpdNdsDeductionSaveRequestRestDto
    {
        /// <summary>
        /// Идентификатор авансового счета-фактуры
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Сумма вычета
        /// </summary>
        public decimal Sum { get; set; }
    }
}