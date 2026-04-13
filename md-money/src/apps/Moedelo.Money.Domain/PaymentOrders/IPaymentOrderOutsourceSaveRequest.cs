using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders;

public interface IPaymentOrderOutsourceSaveRequest
{
    public long DocumentBaseId { get; set; }
    public OperationState OperationState { get; set; }
    public OutsourceState? OutsourceState { get; set; }
}