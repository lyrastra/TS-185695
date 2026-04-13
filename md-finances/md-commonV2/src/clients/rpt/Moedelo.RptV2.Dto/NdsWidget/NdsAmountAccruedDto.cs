namespace Moedelo.RptV2.Dto.NdsWidget
{
    public class NdsAmountAccruedDto
    {
        /// <summary>
        /// НДС при реализации
        /// </summary>
        public decimal NdsOnSales { get; set; }
        
        /// <summary>
        /// НДС к вычету
        /// </summary>
        public decimal NdsDeduction { get; set; }
    }
}