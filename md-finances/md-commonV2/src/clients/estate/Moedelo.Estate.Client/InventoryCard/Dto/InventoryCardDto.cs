using Moedelo.Accounting.Domain.Models.Estate.Enums;
using Moedelo.Common.Enums.Enums.Accounting;
using System;

namespace Moedelo.Estate.Client.InventoryCard.Dto
{
    public class InventoryCardDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string FixedAssetName { get; set; }
        public string InventoryNumber { get; set; }
        public string Okof { get; set; }
        public decimal? CadastralCost { get; set; }
        public long? FixedAssetInvestmentId { get; set; }
        public bool UseAmortization { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Condition { get; set; }
        public bool ProvideInAccounting { get; set; }
        public long? DocumentBaseId { get; set; }
        public EstateType EstateType { get; set; }
        public AccountingCostType AccountingCostType { get; set; }
        public bool HasPropertyTax { get; set; }
        public ProvidePostingType TaxPostingType { get; set; }
        public long? SubcontoId { get; set; }
        public DateTime? DismissalDate { get; set; }
        public InventoryCardAccountingDescriptionDto AccountingDescription { get; set; }
        public InventoryCardTaxDescriptionDto TaxDescription { get; set; }
        public string SubcontoName { get; set; }
        public SubcontoType SubcontoType { get; set; }
        public DateTime? BuyoutDate { get; set; }
    }
}
