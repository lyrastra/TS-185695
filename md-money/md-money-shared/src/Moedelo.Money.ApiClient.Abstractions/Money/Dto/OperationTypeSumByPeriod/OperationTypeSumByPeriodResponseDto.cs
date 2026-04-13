using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto.OperationTypeSumByPeriod;

/// <summary>
/// Агрегированная модель суммы по типу операции за период
/// </summary>
public class OperationTypeSumByPeriodResponseDto
{
    /// <summary>
    /// Тип платежа
    /// </summary>
    public OperationType Type { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    public MonthPeriodDto Period { get; set; }

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
    public AcquiringCommissionSumByPeriodDto[] AcquiringCommissions { get; set; }

    /// <summary>
    /// Сумма из поля "в т.ч. по банковским картам" за период <see cref="Period"/>
    /// </summary>
    public decimal? PaidCardSum { get; set; }
}

