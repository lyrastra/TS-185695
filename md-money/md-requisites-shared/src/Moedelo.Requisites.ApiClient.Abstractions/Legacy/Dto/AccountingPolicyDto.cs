using Moedelo.Requisites.Enums.AccountingPolicy;
using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto
{
    public class AccountingPolicyDto
    {
        public long Id { get; set; }

        public TaxationSystemType TaxationSystem { get; set; }

        public int Year { get; set; }

        public bool IsSmallBusiness { get; set; }
        
        public IncomeTaxPaymentProcedure TaxPaymentProcedure { get; set; }

        public UsnType ObjectOfTaxation { get; set; }

        public decimal? TaxRate { get; set; }

        public int? AnotherIndexCriterionInPercentage { get; set; }

        public bool AreRentAndIntellectualPropertyCoreActivities { get; set; }

        public bool IsMaterialsSettingsActive { get; set; }

        public bool IsProductsSettingsActive { get; set; }

        public bool IsUsedGoodsDeliveryExpenses { get; set; }

        public bool IsUnfinishedManufacturing { get; set; }

        /// <summary>
        /// Убытки прошлых лет
        /// </summary>
        public bool HasPreviousYearsLosses { get; set; }
    }
}