using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.Registry.OperationTypeSumByPeriod;

/// <summary>
/// Агрегированная модель суммы по типу операции за период
/// </summary>
public class OperationTypeSumByPeriodResponse
{
    /// <summary>
    /// Тип платежа
    /// </summary>
    public OperationType Type { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    public MonthPeriod Period { get; set; }

    /// <summary>
    /// Тип: поступление/списание
    /// 1 - Списание
    /// 2 - Поступление
    /// </summary>
    public MoneyDirection Direction { get; set; }

    /// <summary>
    /// Сумма за период <see cref="Period"/>
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// Коллекция сумм комиссий эквайринга за период
    /// </summary>
    /// <remarks>для операции эквайринга (<seealso cref="Type"/> = <seealso cref="OperationType.MemorialWarrantRetailRevenue"/>)</remarks>
    public AcquiringCommissionSumByPeriod[] AcquiringCommissions { get; set; }

    /// <summary>
    /// Сумма из поля "в т.ч. по банковским картам" за период <see cref="Period"/>
    /// </summary>
    public decimal? PaidCardSum { get; set; }
}

