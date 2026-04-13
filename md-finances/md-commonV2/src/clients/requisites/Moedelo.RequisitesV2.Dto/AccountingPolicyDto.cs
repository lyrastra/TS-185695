using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.AccountingPolicy;
using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.RequisitesV2.Dto
{
    public class AccountingPolicyDto
    {
        public long Id { get; set; }

        public TaxationSystemType TaxationSystem { get; set; }

        public int Year { get; set; }

        public bool IsSmallBusiness { get; set; }

        public TypeOfWorkingChartOfAccounts TypeOfWorkingChartOfAccounts { get; set; }

        public AccountingMethod AccountingMethod { get; set; }

        public IncomeTaxPaymentProcedure TaxPaymentProcedure { get; set; }

        public CudirRegister CudirRegister { get; set; }

        public UsnTypes ObjectOfTaxation { get; set; }

        public decimal? TaxRate { get; set; }

        public AccountingMethodForCostOfFixedAsset AccountingMethodForCostOfFixedAsset { get; set; }

        public EstimateMethodForAccountingObjects EstimateMethodForAccountingObjects { get; set; }

        public PeriodicityOfIntermReporting? PeriodicityOfIntermReporting { get; set; }

        public FormOfAccountingReport FormOfAccountingReport { get; set; }

        public CriterionForAccountingIndexes CriterionForAccountingIndexes { get; set; }

        public int? AnotherIndexCriterionInPercentage { get; set; }

        public AccountingMethodForAdministrativeCosts? AccountingMethodForAdministrativeCosts { get; set; }

        public AllocationMethodForIndirectCosts? AllocationMethodForIndirectCosts { get; set; }

        public AllocationMethodForCostsAreNotRelatedToSpecificActivity AllocationMethodForCostsAreNotRelatedToSpecificActivity { get; set; }

        public AllocationMethodForCostsInCaseOfProvisionOfService AllocationMethodForCostsInCaseOfProvisionOfService { get; set; }

        public bool AreRentAndIntellectualPropertyCoreActivities { get; set; }

        public AccountingMethodForEarmarkedFunds AccountingMethodForEarmarkedFunds { get; set; }

        public NdsExamption NdsExamption { get; set; }

        public MethodOfLoggingToBookOfPurchasesAndOthes MethodOfLoggingToBookOfPurchasesAndOthes { get; set; }

        public LimitCostOfAssets LimitCostOfAssets { get; set; }

        public UsefulLifeOfFixedAssets UsefulLifeOfFixedAssets { get; set; }

        public AmortizationMethodForFixedAssets AmortizationMethodForFixedAssets { get; set; }

        public DeterminatioOfUsefulLifeOfSecondHandFixedAssets DeterminatioOfUsefulLifeOfSecondHandFixedAssets { get; set; }

        public RevaluationMethod RevaluationMethodForFixedAssets { get; set; }

        public bool IsMaterialsSettingsActive { get; set; }

        public EstimateMethodForMaterialsCost EstimateMethodForMaterialsCost { get; set; }

        public AccountingMethodForMaterialsTzrForAccounting AccountingMethodForMaterialsTzrForAccounting { get; set; }

        public AccountingMethodForMaterialsTzrForTaxAccounting AccountingMethodForMaterialsTzrForTaxAccounting { get; set; }

        public bool IsProductsSettingsActive { get; set; }

        public EstimateMethodForProductsCost EstimateMethodForProductsCost { get; set; }

        public AccountingMethodForProductsTzrForAccounting AccountingMethodForProductsTzrForAccounting { get; set; }

        public AccountingMethodForProductsTzrForTaxAccounting AccountingMethodForProductsTzrForTaxAccounting { get; set; }

        public EstimateMethodOfMpzForWriteOffForAccounting EstimateMethodOfMpzForWriteOffForAccounting { get; set; }

        public EstimateMethodOfMpzForWriteOffForTaxAccounting EstimateMethodOfMpzForWriteOffForTaxAccounting { get; set; }

        public ForWriteOffMpzByAverageCostOrFifo ForWriteOffMpzByAverageCostOrFifo { get; set; }

        public bool IsUsedGoodsDeliveryExpenses { get; set; }

        public bool IsUnfinishedManufacturing { get; set; }

        public AdditionalPaymentCalculationType? AdditionalPaymentCalculationType { get; set; }

        /// <summary>
        /// Ставка налога за 1 квартал
        /// </summary>
        public decimal? TaxRateQuarter1 { get; set; }

        /// <summary>
        /// Ставка налога за 2 квартал
        /// </summary>
        public decimal? TaxRateQuarter2 { get; set; }

        /// <summary>
        /// Ставка налога за 3 квартал
        /// </summary>
        public decimal? TaxRateQuarter3 { get; set; }

        /// <summary>
        /// Ставка налога за 4 квартал
        /// </summary>
        public decimal? TaxRateQuarter4 { get; set; }

        /// <summary>
        /// Обоноснование пониженной ставки
        /// </summary>
        public string ReasonReduceRate { get; set; }

        /// <summary>
        /// Убытки прошлых лет
        /// </summary>
        public bool HasPreviousYearsLosses { get; set; }
    }
}
