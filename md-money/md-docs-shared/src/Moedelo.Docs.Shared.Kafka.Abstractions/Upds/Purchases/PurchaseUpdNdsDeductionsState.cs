namespace Moedelo.Docs.Shared.Kafka.Abstractions.Upds.Purchases
{
    public class PurchaseUpdNdsDeductionsState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Принять НДС к вычету (при статусе УПД = 1 (док-т и сч-ф) вычеты НДС в самом УПД)
        /// </summary>
        public PurchaseUpdNewState.NdsDeduction[] NdsDeductions { get; set; }
    }
}