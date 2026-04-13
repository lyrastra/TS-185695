using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Estate.ApiClient.Abstractions.legacy.InventoryCards.Dto;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class RentPaymentMapper
    {
        internal static RentPaymentStateDefinition.State MapToState(
            this RentPaymentCreatedMessage eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            InventoryCardDto inventoryCard)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                FixedAssetName = MapToName(inventoryCard),
                InventoryCardBaseId = eventData.InventoryCardBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                RentPeriods = eventData.RentPeriods
                    ?.Select(x => x.MapToDefinitionState())
                    .ToArray(),
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static RentPaymentStateDefinition.State MapToState(
            this RentPaymentUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            InventoryCardDto inventoryCard)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                FixedAssetName = MapToName(inventoryCard),
                InventoryCardBaseId = eventData.InventoryCardBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                RentPeriods = eventData.RentPeriods
                    ?.Select(x => x.MapToDefinitionState())
                    .ToArray(),
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static RentPaymentStateDefinition.State MapToState(
            this RentPaymentDeletedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }

        private static string MapToName(InventoryCardDto inventoryCard)
        {
            if (inventoryCard == null)
            {
                return null;
            }
            return inventoryCard.FixedAssetName;
        }

        public static RentPaymentStateDefinition.RentPeriodInfo MapToDefinitionState(
            this RentPeriod rentPeriod)
        {
            return new()
            {
                Name = rentPeriod.Description,
                Sum = MoneySum.InRubles(rentPeriod.Sum)
            };
        }
    }
}
