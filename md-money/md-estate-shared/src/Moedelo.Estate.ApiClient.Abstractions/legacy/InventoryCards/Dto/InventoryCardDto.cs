using System;

namespace Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto
{
    public class InventoryCardDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public long DocumentBaseId { get; set; }
        public InventoryCardTaxDescriptionDto TaxDescription { get; set; }
        public string FixedAssetName { get; set; }
        public string InventoryNumber { get; set; }
        public long? SubcontoId { get; set; }
    }
}
