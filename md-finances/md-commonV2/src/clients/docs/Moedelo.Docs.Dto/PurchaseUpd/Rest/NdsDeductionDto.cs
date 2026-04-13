using System;

namespace Moedelo.Docs.Dto.PurchaseUpd.Rest
{
    public class NdsDeductionDto
    {
        public long? Id { get; set; }
        
        /// <summary>
        /// Дата принятия к вычету
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Сумма принятая к вычету
        /// </summary>
        public decimal Sum { get; set; }
    }
}