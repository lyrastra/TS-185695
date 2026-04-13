using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Validation
{
    public enum ValidationRuleGroup
    {
        [Description("")]
        None = 0,
        [Description("Настройки фирмы")]
        FirmRequisitesData,
        [Description("Основные сведения")]
        MainData,
        [Description("Данные листка нетрудоспособности")]
        SickListData,
        [Description("Доход и расчет пособия")]
        IncomeAndCalculationData
    }
}