using System.ComponentModel;

namespace Moedelo.Payroll.Shared.Enums.WorkerContractSetting;

/// <summary>
/// Тип  договора ГПД
/// </summary>
public enum WorkerGpdType
{
    [Description("Работы и услуги")]
    WorkContractWork = 0,

    [Description("Аренда имущества")]
    WorkContractOwnership,

    [Description("Проценты по займам")]
    WorkContractInterestOnLoans,

    [Description("Другое")]
    WorkContractOther
}