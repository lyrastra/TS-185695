namespace Moedelo.Docs.Dto.Upd
{
    public class LinkedPaymentDto
    {
        /// <summary>
        /// BaseId УПД 
        /// </summary>
        public long UpdBaseId { get; set; }

        /// <summary>
        /// Базовый документ платежа
        /// </summary>
        public BaseDocumentDto Payment { get; set; }
        
        /// <summary>
        /// Сумма связи
        /// </summary>
        public decimal LinkSum { get; set; }
    }
}