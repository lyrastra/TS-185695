namespace Moedelo.Docs.Dto.PurchaseUpd.Rest
{
    public class LinkedAdvanceInvoiceSaveRequestDto
    {
        /// <summary>
        /// Идентификатор счета-фактуры
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Сумма НДС к восстановлению
        /// </summary>
        public decimal Sum { get; set; }
    }
}