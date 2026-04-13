using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.DataAccess.OperationTypeSumByPeriod.DbModels;

public class OperationTypeSumByPeriodDbModel
{
    /// <summary>
    /// Тип: поступление/списание
    /// 1 - Списание
    /// 2 - Поступление
    /// </summary>
    public MoneyDirection Direction { get; set; }

    /// <summary>
    /// Тип платежа
    /// </summary>
    public OperationType Type { get; set; }

    /// <summary>
    /// Период: Год операции
    /// </summary>
    public int OperationDateYear { get; set; }

    /// <summary>
    /// Период: Номер месяца операции
    /// </summary>
    public int OperationDateMonth { get; set; }

    /// <summary>
    /// Сумма за период (<see cref="OperationDateYear"/> и <see cref="OperationDateMonth"/>)
    /// </summary>
    /// <remarks>для валютных операций сумма в рублях</remarks>
    public decimal OperationSum { get; set; }

    /// <summary>
    /// Период: Год комисси за операцию эквайринга
    /// </summary>
    public int? AcquiringCommissionDateYear { get; set; }

    /// <summary>
    /// Период: Номер месяца за операцию эквайринга
    /// </summary>
    public int? AcquiringCommissionDateMonth { get; set; }

    /// <summary>
    /// Сумма комиссии за период для операции эквайринга (<see cref="AcquiringCommissionDateYear"/> и <see cref="AcquiringCommissionDateMonth"/>)
    /// </summary>
    public decimal? AcquiringCommissionSum { get; set; }

    /// <summary>
    /// Сумма из поля "в т.ч. по банковским картам" за период (<see cref="OperationDateYear"/> и <see cref="OperationDateMonth"/>)
    /// </summary>
    public decimal? PaidCardSum { get; set; }
}
