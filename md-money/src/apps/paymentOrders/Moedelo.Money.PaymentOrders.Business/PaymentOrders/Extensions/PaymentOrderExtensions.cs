using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Exceptions;
using Moedelo.Money.PaymentOrders.Domain;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Extensions
{
    internal static class PaymentOrderExtensions
    {
        public static void CheckExistence(this PaymentOrder paymentOrder, long documentBaseId)
        {
            if (paymentOrder == null)
            {
                throw new PaymentOrderNotFoundExcepton(documentBaseId);
            }
        }

        public static void CheckType(this PaymentOrder paymentOrder, OperationType operationType)
        {
            if (paymentOrder.OperationType != operationType)
            {
                throw new PaymentOrderMismatchTypeExcepton
                {
                    DocumentBaseId = paymentOrder.DocumentBaseId,
                    ExpectedType = operationType,
                    ActualType = paymentOrder.OperationType
                };
            }
        }

        public static PaymentOrderSnapshot ToSnapshot(this PaymentOrder paymentOrder)
        {
            return !string.IsNullOrEmpty(paymentOrder.PaymentSnapshot)
                ? XmlHelper.Deserialize<PaymentOrderSnapshot>(paymentOrder.PaymentSnapshot)
                : null;
        }
    }
}