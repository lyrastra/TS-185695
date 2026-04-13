using System;

namespace Moedelo.Estate.Client.InventoryCard.Dto
{
    public class InventoryCardFromBizToAccTransferSaveDto
    {
        public DateTime Date { get; set; }
        public string FixedAssetName { get; set; }
        public string InventoryNumber { get; set; }
        public string Okof { get; set; }
        public string ResponsiblePerson { get; set; }
        public decimal Cost { get; set; }
        public int UsefulLife { get; set; }
        public bool UseAmortization { get; set; }
        public decimal AmortizationInBalance { get; set; }
    }
}