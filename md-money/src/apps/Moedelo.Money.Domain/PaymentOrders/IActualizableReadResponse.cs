using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders
{
    public interface IActualizableReadResponse
    {
        long DocumentBaseId { get; }
        DateTime Date { get; }
        bool IsPaid { get; }
        OperationState OperationState { get; }
    }
}
