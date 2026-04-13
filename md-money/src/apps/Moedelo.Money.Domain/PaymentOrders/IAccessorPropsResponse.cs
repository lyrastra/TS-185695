using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders;

/// <summary>
/// Набор полей для определения доступа к п/п
/// </summary>
public interface IAccessorPropsResponse
{
    bool ProvideInAccounting { get; }
    /// <summary>
    /// Признак: на обработке в Ауте ("жёлтая таблица")
    /// </summary>
    public OutsourceState? OutsourceState { get; set; }
}