using System.ComponentModel;

namespace Moedelo.Payroll.Shared.Enums.WorkerContractSetting
{
    public enum WorkerContractType
    {
        [Description("Договор о выполнении работ/оказании услуг (не начисляются взносы на травматизм)")]
        WorkOrService = 0,

        [Description("Договор авторского заказа (не начисляются взносы на травматизм)")]
        Copyright = 1,

        [Description("Договор об отчуждении исключительного права на произведения науки, литературы, искусства")]
        AlienationExclusiveRight = 2,

        [Description("Издательский лицензионный договор")]
        Publishing = 3,

        [Description("Лицензионный договор о предоставлении права использования произведения науки, литературы, искусства")]
        License = 4,

        [Description("Договор о выполнении работ/оказании услуг (начисляются взносы на травматизм)")]
        WorkOrServiceWithInjuredTax = 5,

        [Description("Договор авторского заказа (начисляются взносы на травматизм)")]
        CopyrightWithInjuredTax = 6,
    }
}