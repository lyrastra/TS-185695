namespace Moedelo.CheckVerification.Client.Receipts.Models
{
    public class ReceiptItemDto
    {
        /// <summary>
        /// Наименование позиции
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Цена (с НДС, если есть)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Сумма (с НДС, если есть)
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Ставка (процент) НДС 
        /// </summary>
        public int? NdsRate { get; set; }
        
        public int? ProductType { get; set; }
        
        public int? PaymentType { get; set; }
    }
}