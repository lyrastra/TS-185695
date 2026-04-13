using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Infrastructure;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class OperationTypeAttribute : Attribute
{
    public OperationType OperationType { get; }

    public OperationTypeAttribute(OperationType operationType)
    {
        OperationType = operationType;
    }
}