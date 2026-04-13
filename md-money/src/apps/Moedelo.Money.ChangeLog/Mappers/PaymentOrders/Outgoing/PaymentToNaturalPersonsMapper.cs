using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class PaymentToNaturalPersonsMapper
    {
        internal static PaymentToNaturalPersonsStateDefinition.State MapToState(
            this PaymentToNaturalPersonsCreated eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Description = eventData.Description,
                PaymentType = eventData.PaymentType.GetDescription(),
                IsPaid = eventData.IsPaid,
                Sum = MoneySum.InRubles(eventData.Sum),
                EmployeePayments = eventData.EmployeePayments
                    .SelectMany(x => x.MapToDefinitionState())
                    .ToArray(),

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static PaymentToNaturalPersonsStateDefinition.State MapToState(
            this PaymentToNaturalPersonsUpdated eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Description = eventData.Description,
                PaymentType = eventData.PaymentType.GetDescription(),
                IsPaid = eventData.IsPaid,
                Sum = MoneySum.InRubles(eventData.Sum),
                EmployeePayments = eventData.EmployeePayments
                    .SelectMany(x => x.MapToDefinitionState())
                    .ToArray(),

                ProvideInAccounting = eventData.ProvideInAccounting
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
