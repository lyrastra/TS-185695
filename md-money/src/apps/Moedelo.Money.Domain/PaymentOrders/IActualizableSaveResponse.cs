using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders;

public interface IActualizableSaveRequest
{
    public OperationState OperationState { get; set; }
}