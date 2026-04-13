using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToNaturalPersons.Events;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class PaymentToNaturalPersonsMapper
    {
        internal static PaymentToNaturalPersonsStateDefinition.State MapToState(
            this PaymentToNaturalPersonsCreated eventData,
            CashDto cash)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                CashId = eventData.CashId,
                CashName = cash?.Name,
                Destination = eventData.Destination,
                //Comment = eventData.Comment, ARCH-352 не показываем поле
                PaymentType = eventData.PaymentType.GetDescription(),
                Sum = MoneySum.InRubles(eventData.Sum),
                EmployeePayments = eventData.EmployeePayments
                    .SelectMany(x => x.MapToDefinitionState())
                    .ToArray(),

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static PaymentToNaturalPersonsStateDefinition.State MapToState(
            this PaymentToNaturalPersonsUpdated eventData,
            CashDto cash)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                CashId = eventData.CashId,
                CashName = cash?.Name,
                Destination = eventData.Destination,
                //Comment = eventData.Comment, ARCH-352 не показываем поле
                PaymentType = eventData.PaymentType.GetDescription(),
                Sum = MoneySum.InRubles(eventData.Sum),
                EmployeePayments = eventData.EmployeePayments
                    .SelectMany(x => x.MapToDefinitionState())
                    .ToArray(),

                ProvideInAccounting = eventData.ProvideInAccounting,

                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingPaymentForWorking
                    ? eventData.OldOperationType.GetDescription()
                    : null,
            };
        }

        internal static PaymentToNaturalPersonsStateDefinition.State MapToState(
            this PaymentToNaturalPersonsDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
