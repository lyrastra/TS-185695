using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System;
using System.Linq;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    internal static class RentPaymentMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(RentPaymentSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static RentPaymentDto MapToDto(RentPaymentSaveRequest operation)
        {
            return new RentPaymentDto
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Sum = operation.Sum,
                SettlementAccountId = operation.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisitesToDto(operation.Kontragent),
                IncludeNds = operation.IncludeNds,
                NdsType = operation.NdsType,
                NdsSum = operation.NdsSum,
                Description = operation.Description,
                ProvideInAccounting = operation.ProvideInAccounting,
                PostingsAndTaxMode = Enums.ProvidePostingType.Auto,
                TaxPostingType = Enums.ProvidePostingType.Auto,
                IsPaid = operation.IsPaid,
                RentPeriods = operation.RentPeriods.Select(MapRentPeriod).ToArray(),
                OperationState = operation.OperationState,
                OutsourceState = operation.OutsourceState,
            };
        }

        internal static RentPaymentCreatedMessage MapToCreatedMessage(RentPaymentSaveRequest operation)
        {
            return new RentPaymentCreatedMessage
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Sum = operation.Sum,
                Nds = operation.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = operation.NdsSum,
                        NdsType = operation.NdsType
                    }
                    : null,
                SettlementAccountId = operation.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(operation.Kontragent),
                ContractBaseId = operation.ContractBaseId,
                InventoryCardBaseId = operation.InventoryCardBaseId,
                Description = operation.Description,
                IsPaid = operation.IsPaid,
                ProvideInAccounting = operation.ProvideInAccounting,
                RentPeriods = (operation.RentPeriods ?? Array.Empty<RentPeriod>())
                    .Select(MapRentPeriodToKafka)
                    .ToArray(),
                OperationState = operation.OperationState,
                OutsourceState = operation.OutsourceState,
                IsSaveNumeration = operation.IsSaveNumeration,
                ImportRuleIds = operation.ImportRuleIds,
                ImportLogId = operation.ImportLogId,
            };
        }

        internal static RentPaymentUpdatedMessage MapToUpdatedMessage(RentPaymentSaveRequest operation)
        {
            return new RentPaymentUpdatedMessage
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Number = operation.Number,
                Sum = operation.Sum,
                Nds = operation.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = operation.NdsSum,
                        NdsType = operation.NdsType
                    }
                    : null,
                SettlementAccountId = operation.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(operation.Kontragent),
                ContractBaseId = operation.ContractBaseId,
                InventoryCardBaseId = operation.InventoryCardBaseId,
                Description = operation.Description,
                IsPaid = operation.IsPaid,
                ProvideInAccounting = operation.ProvideInAccounting,
                RentPeriods = (operation.RentPeriods ?? Array.Empty<RentPeriod>())
                    .Select(MapRentPeriodToKafka)
                    .ToArray(),
                OperationState = operation.OperationState,
                OutsourceState = operation.OutsourceState,
            };
        }

        internal static RentPaymentDeletedMessage MapToDeletedMessage(RentPaymentResponse response, long? newDocumentBaseId)
        {
            return new RentPaymentDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static RentPaymentResponse MapToResponse(RentPaymentDto dto)
        {
            return new RentPaymentResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                IncludeNds = dto.IncludeNds,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                RentPeriods = dto.RentPeriods.Select(MapRentPeriod).ToArray(),
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        private static RentPeriodDto MapRentPeriod(RentPeriod period)
        {
            return new RentPeriodDto
            {
                PaymentSum = period.PaymentSum,
                RentalPaymentItemId = period.RentalPaymentItemId
            };
        }

        private static RentPeriod MapRentPeriod(RentPeriodDto dto)
        {
            return new RentPeriod
            {
                PaymentSum = dto.PaymentSum,
                RentalPaymentItemId = dto.RentalPaymentItemId,
                PaymentRequiredSum = dto.PaymentRequiredSum
            };
        }

        private static Kafka.Abstractions.Models.RentPeriod MapRentPeriodToKafka(RentPeriod period)
        {
            return new Kafka.Abstractions.Models.RentPeriod
            {
                Id = period.RentalPaymentItemId,
                Sum = period.PaymentSum,
                Description = period.Description
            };
        }

        public static RentPaymentSaveRequest Map(RentPaymentResponse response)
        {
            return new RentPaymentSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Number = response.Number,
                Date = response.Date,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Kontragent = response.Kontragent,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                InventoryCardBaseId = response.InventoryCard.GetOrThrow()?.DocumentBaseId,
                Description = response.Description,
                IncludeNds = response.IncludeNds,
                NdsSum = response.NdsSum,
                NdsType = response.NdsType,
                RentPeriods = response.RentPeriods,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        public static RentPaymentSaveRequest MapToSaveRequest(RentPaymentImportRequest request)
        {
            return new RentPaymentSaveRequest
            {
                Number = request.Number,
                Date = request.Date,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Kontragent = request.Kontragent,
                ContractBaseId = request.ContractBaseId,
                ProvideInAccounting = true,
                IsPaid = true,
                InventoryCardBaseId = null,
                Description = request.Description,
                RentPeriods = Array.Empty<RentPeriod>(),
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IncludeNds = false,
                NdsSum = null,
                NdsType = null,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }
    }
}
