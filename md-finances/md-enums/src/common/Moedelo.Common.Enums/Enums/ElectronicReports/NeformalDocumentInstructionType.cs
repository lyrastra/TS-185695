using Moedelo.Common.Enums.Attributes;
using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum NeformalDocumentInstructionType
    {
        /// <summary>Тип по-умолчанию</summary> 
        [Description("Тип по-умолчанию")]
        Undefined = 0,

        /// <summary>Требование по взносам за сотрудников</summary>
        [Description("Требование по взносам за сотрудников")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.InsurancePaymentsUntill2017,
            NeformalDocumentConditionType.InsurancePaymentsSince2017,
            NeformalDocumentConditionType.InsurancePenaltiesUntill2017,
            NeformalDocumentConditionType.InsurancePenaltiesSince2017,
            NeformalDocumentConditionType.InsuranceFinesUntill2017,
            NeformalDocumentConditionType.InsuranceFinesSince2017,
            NeformalDocumentConditionType.FomsPaymentsUntill2017,
            NeformalDocumentConditionType.FomsPaymentsSince2017,
            NeformalDocumentConditionType.FomsPenaltiesUntill2017,
            NeformalDocumentConditionType.FomsPenaltiesSince2017,
            NeformalDocumentConditionType.FomsFinesUntill2017,
            NeformalDocumentConditionType.FomsFinesSince2017,
            NeformalDocumentConditionType.FssPaymentsUntill2017,
            NeformalDocumentConditionType.FssPaymentsSince2017,
            NeformalDocumentConditionType.FssPenaltiesUntill2017,
            NeformalDocumentConditionType.FssPenaltiesSince2017,
            NeformalDocumentConditionType.FssFinesUntill2017,
            NeformalDocumentConditionType.FssFinesSince2017
        )]
        InsuranceTax,

        /// <summary>Требование по НДС</summary>
        [Description("Требование по НДС")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.NdsTax,
            NeformalDocumentConditionType.NdsTaxPenalties,
            NeformalDocumentConditionType.NdsTaxFines
        )]
        NdsTax,

        /// <summary>Требование по налогу на прибыль</summary>
        [Description("Требование по налогу на прибыль")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.ProfitTaxToFederalBudget,
            NeformalDocumentConditionType.ProfitTaxToFederalBudgetPenalties,
            NeformalDocumentConditionType.ProfitTaxToFederalBudgetFines,
            NeformalDocumentConditionType.ProfitTaxToRegionalBudget,
            NeformalDocumentConditionType.ProfitTaxToRegionalBudgetPenalties,
            NeformalDocumentConditionType.ProfitTaxToRegionalBudgetFines
        )]
        ProfitTax,

        /// <summary>Требование по УСН</summary>
        [Description("Требование по УСН")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.Usn6Tax,
            NeformalDocumentConditionType.Usn6TaxPenalties,
            NeformalDocumentConditionType.Usn6TaxFines
        )]
        Usn6Tax,

        /// <summary>Требование по УСН</summary>
        [Description("Требование по УСН")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.Usn15Tax,
            NeformalDocumentConditionType.Usn15TaxPenalties,
            NeformalDocumentConditionType.Usn15TaxFines
        )]
        Usn15Tax,

        /// <summary>Требование по ЕНВД</summary>
        [Description("Требование по ЕНВД")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.EnvdTax,
            NeformalDocumentConditionType.EnvdTaxPenalties,
            NeformalDocumentConditionType.EnvdTaxFines
        )]
        EnvdTax,

        /// <summary>Требование по фиксированным взносам</summary>
        [Description("Требование по фиксированным взносам")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.FixedPaymentsToPfrUntill2017,
            NeformalDocumentConditionType.FixedPaymentsToPfrPenaltiesUntill2017,
            NeformalDocumentConditionType.FixedPaymentsToPfrFinesUntill2017
        )]
        FixedPaymentsToPfrUntill2017,

        /// <summary>Требование по фиксированным взносам</summary>
        [Description("Требование по фиксированным взносам")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.FixedPaymentsToPfrSince2017,
            NeformalDocumentConditionType.FixedPaymentsToPfrPenaltiesSince2017,
            NeformalDocumentConditionType.FixedPaymentsToPfrFinesSince2017
        )]
        FixedPaymentsToPfrSince2017,

        /// <summary>Требование по НДФЛ</summary>
        [Description("Требование по НДФЛ")]
        [NeformalDocumentInstructionType(
            NeformalDocumentConditionType.NdflTax,
            NeformalDocumentConditionType.NdflTaxPenalties,
            NeformalDocumentConditionType.NdflTaxFines
        )]
        NdflTax,

        /// <summary>Требование по отчету "Расчет по страховым взносам"</summary>
        [Description("Требование по отчету \"Расчет по страховым взносам\"")]
        [NeformalDocumentInstructionType(NeformalDocumentConditionType.BasedOnRsv)]
        BasedOnRsv,

        /// <summary>Требование по отчету "6-НДФЛ"</summary>
        [Description("Требование по отчету \"6-НДФЛ\"")]
        [NeformalDocumentInstructionType(NeformalDocumentConditionType.BasedOn6Ndfl)]
        BasedOn6Ndfl,

        /// <summary>Требование по отчету "Декларация НДС"</summary>
        [Description("Требование по отчету \"Декларация НДС\"")]
        [NeformalDocumentInstructionType(NeformalDocumentConditionType.BasedOnNdsDeclaration)]
        BasedOnNdsDeclaration,

        /// <summary>Требование по отчету "Декларация по УСН"</summary>
        [Description("Требование по отчету \"Декларация по УСН\"")]
        [NeformalDocumentInstructionType(NeformalDocumentConditionType.BasedOnUsnDeclaration)]
        BasedOnUsnDeclaration,

        /// <summary>Требование по отчету "Декларация по налогу на прибыль"</summary>
        [Description("Требование по отчету \"Декларация по налогу на прибыль\"")]
        [NeformalDocumentInstructionType(NeformalDocumentConditionType.BasedOnProfitTax)]
        BasedOnProfitTax,

        /// <summary>Требование по отчету "Декларация по ЕНВД"</summary>
        [Description("Требование по отчету \"Декларация по ЕНВД\"")]
        [NeformalDocumentInstructionType(NeformalDocumentConditionType.BasedOnEnvd)]
        BasedOnEnvd
    }
}
