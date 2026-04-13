namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment

{
    /// <summary>
    /// Инвентарная карта основного средства за которое вносится арендный платеж
    /// </summary>
    public class InventoryCard
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Название ОС
        /// </summary>
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Инвентарный номер
        /// </summary>
        public string InventoryNumber { get; set; }
    }
}
