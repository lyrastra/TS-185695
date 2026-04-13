using Moedelo.Money.Business.Abstractions.Commands.CashOrders;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders;
using Moedelo.Money.Business.Abstractions.Commands.PurseOperations;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Operations
{
    internal static class CommandMapper
    {
        internal static PaymentOrderChangeTaxationSystemCommand MapPaymentOrderCommand(long documentBaseId, TaxationSystemType taxationSystemType, Guid guid)
        {
            return new PaymentOrderChangeTaxationSystemCommand
            {
                DocumentBaseId = documentBaseId,
                TaxationSystemType = taxationSystemType,
                Guid = guid
            };
        }

        internal static CashOrderChangeTaxationSystemCommand MapCashOrderCommand(long documentBaseId, TaxationSystemType taxationSystemType, Guid guid)
        {
            return new CashOrderChangeTaxationSystemCommand
            {
                DocumentBaseId = documentBaseId,
                TaxationSystemType = taxationSystemType,
                Guid = guid
            };
        }

        internal static PurseOperationChangeTaxationSystemCommand MapPurseOperationCommand(long documentBaseId, TaxationSystemType taxationSystemType, Guid guid)
        {
            return new PurseOperationChangeTaxationSystemCommand
            {
                DocumentBaseId = documentBaseId,
                TaxationSystemType = taxationSystemType,
                Guid = guid,
            };
        }
    }
}
