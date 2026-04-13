namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RentPayment
{
    /// <summary>
    /// Инвентраная карта основного средства за которое вносится арендный платеж
    /// </summary>
    public class InventoryCardResponseDto
    {
        /// <summary>
        /// Идентификатор документа инвентарой карты
        /// </summary>
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
