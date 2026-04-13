using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Training
{
    public enum TrainingType
    {
        [Description("Налоговый календарь")]
        TaxCalendar = 0,
        [Description("Формирование отчётов")]
        Reporting = 1,
        [Description("Раздел Деньги")]
        MoneySection = 2,
        [Description("Раздел Документы")]
        DocumentsSection = 3,
        [Description("Интеграция")]
        Integration = 4,
        [Description("Реквизиты")]
        Requisites = 5,
        [Description("Выпуск/перевыпуск ЭЦП")]
        IssueReissueOfEDS = 6,
        [Description("Раздел Зарплата")]
        SalarySection = 7,
        [Description("Раздел Товары")]
        GoodsSection = 8,
        [Description("Фиксированные взносы")]
        FixedContributions = 9,
        [Description("Внесение остатков")]
        DepositBalance = 10,
        [Description("Другое")]
        Other = 11
    }
}