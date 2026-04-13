using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum NeformalDocumentConditionType
    {
        /// <summary>Тип не распознан</summary>
        [Description("Тип не распознан")] Undefined = 0,
        
        /// <summary>Взносы Страховая часть до 2017 г.</summary>
        [Description("Взносы Страховая часть до 2017 г.")] InsurancePaymentsUntill2017 = 1,
        /// <summary>Взносы Страховая часть c 2017 г.</summary>
        [Description("Взносы Страховая часть c 2017 г.")] InsurancePaymentsSince2017 = 2,
        /// <summary>Пени Страховая часть до 2017 г.</summary>
        [Description("Пени Страховая часть до 2017 г.")] InsurancePenaltiesUntill2017 = 3,
        /// <summary>Пени Страховая часть c 2017 г.</summary>
        [Description("Пени Страховая часть c 2017 г.")] InsurancePenaltiesSince2017 = 4,
        /// <summary>Штрафы Страховая часть до 2017 г.</summary>
        [Description("Штрафы Страховая часть до 2017 г.")] InsuranceFinesUntill2017 = 5,
        /// <summary>Штрафы Страховая часть c 2017 г.</summary>
        [Description("Штрафы Страховая часть c 2017 г.")] InsuranceFinesSince2017 = 6,
        
        /// <summary>Взносы ФОМС до 2017 г.</summary>
        [Description("Взносы ФОМС до 2017 г.")] FomsPaymentsUntill2017 = 7,
        /// <summary>Взносы ФОМС c 2017 г.</summary>
        [Description("Взносы ФОМС c 2017 г.")] FomsPaymentsSince2017 = 8,
        /// <summary>Пени ФОМС до 2017 г.</summary>
        [Description("Пени ФОМС до 2017 г.")] FomsPenaltiesUntill2017 = 9,
        /// <summary>Пени ФОМС c 2017 г.</summary>
        [Description("Пени ФОМС c 2017 г.")] FomsPenaltiesSince2017 = 10,
        /// <summary>Штрафы ФОМС до 2017 г.</summary>
        [Description("Штрафы ФОМС до 2017 г.")] FomsFinesUntill2017 = 11,
        /// <summary>Штрафы ФОМС c 2017 г.</summary>
        [Description("Штрафы ФОМС c 2017 г.")] FomsFinesSince2017 = 12,
        
        /// <summary>Взносы ФСС до 2017 г.</summary>
        [Description("Взносы ФСС до 2017 г.")] FssPaymentsUntill2017 = 13,
        /// <summary>Взносы ФСС c 2017 г.</summary>
        [Description("Взносы ФСС c 2017 г.")] FssPaymentsSince2017 = 14,
        /// <summary>Пени ФСС до 2017 г.</summary>
        [Description("Пени ФСС до 2017 г.")] FssPenaltiesUntill2017 = 15,
        /// <summary>Пени ФСС c 2017 г.</summary>
        [Description("Пени ФСС c 2017 г.")] FssPenaltiesSince2017 = 16,
        /// <summary>Штрафы ФСС до 2017 г.</summary>
        [Description("Штрафы ФСС до 2017 г.")] FssFinesUntill2017 = 17,
        /// <summary>Штрафы ФСС c 2017 г.</summary>
        [Description("Штрафы ФСС c 2017 г.")] FssFinesSince2017 = 18,
        
        /// <summary>Налог НДС</summary>
        [Description("Налог НДС")] NdsTax = 19,
        /// <summary>Пени Налог НДС</summary>
        [Description("Пени Налог НДС")] NdsTaxPenalties = 20,
        /// <summary>Штрафы Налог НДС</summary>
        [Description("Штрафы Налог НДС")] NdsTaxFines = 21,
        
        /// <summary>Налог на прибыль в федеральный бюджет</summary>
        [Description("Налог на прибыль в федеральный бюджет")] ProfitTaxToFederalBudget = 22,
        /// <summary>Пени Налог на прибыль в федеральный бюджет</summary>
        [Description("Пени Налог на прибыль в федеральный бюджет")] ProfitTaxToFederalBudgetPenalties = 23,
        /// <summary>Штрафы Налог на прибыль в федеральный бюджет</summary>
        [Description("Штрафы Налог на прибыль в федеральный бюджет")] ProfitTaxToFederalBudgetFines = 24,
        /// <summary>Налог на прибыль в региональный бюджет</summary>
        [Description("Налог на прибыль в региональный бюджет")] ProfitTaxToRegionalBudget = 25,
        /// <summary>Пени Налог на прибыль в региональный бюджет</summary>
        [Description("Пени Налог на прибыль в региональный бюджет")] ProfitTaxToRegionalBudgetPenalties = 26,
        /// <summary>Штрафы Налог на прибыль в региональный бюджет</summary>
        [Description("Штрафы Налог на прибыль в региональный бюджет")] ProfitTaxToRegionalBudgetFines = 27,
        
        /// <summary>Налог УСН доходы</summary>
        [Description("Налог УСН доходы")] Usn6Tax = 28,
        /// <summary>Пени Налог УСН доходы</summary>
        [Description("Пени Налог УСН доходы")] Usn6TaxPenalties = 29,
        /// <summary>Штрафы Налог УСН доходы</summary>
        [Description("Штрафы Налог УСН доходы")] Usn6TaxFines = 30,
        /// <summary>Налог УСН доходы минус расходы</summary>
        [Description("Налог УСН доходы минус расходы")] Usn15Tax = 31,
        /// <summary>Пени Налог УСН доходы минус расходы</summary>
        [Description("Пени Налог УСН доходы минус расходы")] Usn15TaxPenalties = 32,
        /// <summary>Штрафы Налог УСН доходы минус расходы</summary>
        [Description("Штрафы Налог УСН доходы минус расходы")] Usn15TaxFines = 33,
        
        /// <summary>Налог ЕНВД</summary>
        [Description("Налог ЕНВД")] EnvdTax = 34,
        /// <summary>Пени Налог ЕНВД</summary>
        [Description("Пени Налог ЕНВД")] EnvdTaxPenalties = 35,
        /// <summary>Штрафы Налог ЕНВД</summary>
        [Description("Штрафы Налог ЕНВД")] EnvdTaxFines = 36,
        
        /// <summary>Фиксированные взносы в ПФР до 2017 г.</summary>
        [Description("Фиксированные взносы в ПФР до 2017 г.")] FixedPaymentsToPfrUntill2017 = 37,
        /// <summary>Пени Фиксированные взносы в ПФР до 2017 г.</summary>
        [Description("Пени Фиксированные взносы в ПФР до 2017 г.")] FixedPaymentsToPfrPenaltiesUntill2017 = 38,
        /// <summary>Штрафы Фиксированные взносы в ПФР до 2017 г.</summary>
        [Description("Штрафы Фиксированные взносы в ПФР до 2017 г.")] FixedPaymentsToPfrFinesUntill2017 = 39,
        /// <summary>Фиксированные взносы в ФФОМС до 2017 г.</summary>
        [Description("Фиксированные взносы в ФФОМС до 2017 г.")] FixedPaymentsToFfomsUntill2017 = 40,
        /// <summary>Пени Фиксированные взносы в ФФОМС до 2017 г.</summary>
        [Description("Пени Фиксированные взносы в ФФОМС до 2017 г.")] FixedPaymentsToFfomsPenaltiesUntill2017 = 41,
        /// <summary>Штрафы Фиксированные взносы в ФФОМС до 2017 г.</summary>
        [Description("Штрафы Фиксированные взносы в ФФОМС до 2017 г.")] FixedPaymentsToFfomsFinesUntill2017 = 42,
        /// <summary>Дополнительные взносы до 2017 г.</summary>
        [Description("Дополнительные взносы до 2017 г.")] AdditionalPaymentsUntill2017 = 43,
        
        /// <summary>Фиксированные взносы в ПФР c 2017 г.</summary>
        [Description("Фиксированные взносы в ПФР c 2017 г.")] FixedPaymentsToPfrSince2017 = 44,
        /// <summary>Пени Фиксированные взносы в ПФР c 2017 г.</summary>
        [Description("Пени Фиксированные взносы в ПФР c 2017 г.")] FixedPaymentsToPfrPenaltiesSince2017 = 45,
        /// <summary>Штрафы Фиксированные взносы в ПФР c 2017 г.</summary>
        [Description("Штрафы Фиксированные взносы в ПФР c 2017 г.")] FixedPaymentsToPfrFinesSince2017 = 46,
        /// <summary>Фиксированные взносы в ФФОМС c 2017 г.</summary>
        [Description("Фиксированные взносы в ФФОМС c 2017 г.")] FixedPaymentsToFfomsSince2017 = 47,
        /// <summary>Пени Фиксированные взносы в ФФОМС c 2017 г.</summary>
        [Description("Пени Фиксированные взносы в ФФОМС c 2017 г.")] FixedPaymentsToFfomsPenaltiesSince2017 = 48,
        /// <summary>Штрафы Фиксированные взносы в ФФОМС c 2017 г.</summary>
        [Description("Штрафы Фиксированные взносы в ФФОМС c 2017 г.")] FixedPaymentsToFfomsFinesSince2017 = 49,
        /// <summary>Дополнительные взносы c 2017 г.</summary>
        [Description("Дополнительные взносы c 2017 г.")] AdditionalPaymentsSince2017 = 50,
        
        /// <summary>Налог НДФЛ</summary>
        [Description("Налог НДФЛ")] NdflTax = 51,
        /// <summary>Пени Налог НДФЛ</summary>
        [Description("Пени Налог НДФЛ")] NdflTaxPenalties = 52,
        /// <summary>Штрафы Налог НДФЛ</summary>
        [Description("Штрафы Налог НДФЛ")] NdflTaxFines = 53,
        
        /// <summary>Расчёт по страховым взносам</summary>
        [Description("Расчёт по страховым взносам")] BasedOnRsv = 100,
        /// <summary>Расчет сумм налога на доходы физических лиц, исчисленных и удержанных налоговым агентом (Форма 6-НДФЛ)</summary>
        [Description("Расчет сумм налога на доходы физических лиц, исчисленных и удержанных налоговым агентом (Форма 6-НДФЛ)")] BasedOn6Ndfl = 101,
        /// <summary>Налоговая декларация по налогу на добавленную стоимость</summary>
        [Description("Налоговая декларация по налогу на добавленную стоимость")] BasedOnNdsDeclaration = 102,
        /// <summary>Налоговая декларация по налогу, уплачиваемому в связи с применением упрощенной системы налогообложения</summary>
        [Description("Налоговая декларация по налогу, уплачиваемому в связи с применением упрощенной системы налогообложения")] BasedOnUsnDeclaration = 103,
        /// <summary>Налоговая декларация по налогу на прибыль организаций</summary>
        [Description("Налоговая декларация по налогу на прибыль организаций")] BasedOnProfitTax = 104,
        /// <summary>Налоговая декларация по единому налогу на вмененный доход для отдельных видов деятельности</summary>
        [Description("Налоговая декларация по единому налогу на вмененный доход для отдельных видов деятельности")] BasedOnEnvd = 105,
    }
}