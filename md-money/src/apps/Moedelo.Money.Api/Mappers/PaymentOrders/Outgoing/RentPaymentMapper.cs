using System;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using System.Linq;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Enums;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class RentPaymentMapper
    {

        public static RentPaymentSaveRequest Map(RentPaymentSaveDto dto)
        {
            return new RentPaymentSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.RentPayment
                    : dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : null,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                IsPaid = dto.IsPaid,
                InventoryCardBaseId = dto?.InventoryCard?.DocumentBaseId,
                RentPeriods = dto.RentPeriods.Select(MapRentPeriod).ToArray(),
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }


        public static RentPaymentResponseDto Map(RentPaymentResponse response)
        {
            return new RentPaymentResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                Nds = MapNds(response),
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                InventoryCard = MapInventoryCard(response.InventoryCard),
                RentPeriods = response.RentPeriods.Select(MapRentPeriod).ToArray(),
                IsFromImport = response.IsFromImport
            };
        } 

        private static RemoteServiceResponseDto<InventoryCardResponseDto> MapInventoryCard(RemoteServiceResponse<InventoryCard> response)
        {
            var inventoryCard = response.Data;

            if (inventoryCard == null)
            {
                return new RemoteServiceResponseDto<InventoryCardResponseDto> { Status = response.Status };
            }

            return new RemoteServiceResponseDto<InventoryCardResponseDto>
            {
                Data = new InventoryCardResponseDto
                {
                    DocumentBaseId = inventoryCard.DocumentBaseId,
                    FixedAssetName = inventoryCard.FixedAssetName,
                    InventoryNumber = inventoryCard.InventoryNumber
                },
                Status = response.Status
            };
        }

        private static NdsResponseDto MapNds(RentPaymentResponse operation)
        {
            return new NdsResponseDto
            {
                IncludeNds = operation.IncludeNds,
                Type = operation.IncludeNds
                    ? operation.NdsType
                    : null,
                Sum = operation.IncludeNds
                    ? operation.NdsSum
                    : null
            };
        }
        private static RentPeriod MapRentPeriod(RentPeriodSaveDto dto)
        {
            return new RentPeriod
            {
                PaymentSum = dto.Sum,
                RentalPaymentItemId = dto.Id
            };
        }

        private static RentPeriodResponseDto MapRentPeriod(RentPeriod period)
        {
            return new RentPeriodResponseDto
            {
                Sum = period.PaymentSum,
                Id = period.RentalPaymentItemId,
                Description = period.Description,
                PaymentType = (Models.PaymentOrders.Outgoing.RentPayment.RentPeriodType) period.PaymentType,
                PaymentRequiredSum = period.PaymentRequiredSum
            };
        }

        internal static RentPaymentSaveRequest ToSaveRequest(this ConfirmRentPaymentDto dto)
        {
            return new RentPaymentSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Description = dto.Description,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Date = dto.Date,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                IncludeNds = dto.Nds?.IncludeNds == true,
                NdsSum = dto.Nds?.Sum,
                NdsType = dto.Nds?.Type,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,

                IsPaid = true,
                ProvideInAccounting = true,
                RentPeriods = Array.Empty<RentPeriod>(),
                InventoryCardBaseId = null,
                ContractBaseId = null,
            };
        }
    }
}
