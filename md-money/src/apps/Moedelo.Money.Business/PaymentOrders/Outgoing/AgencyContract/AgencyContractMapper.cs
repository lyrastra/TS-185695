using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract
{
    internal static class AgencyContractMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(AgencyContractSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static AgencyContractDto MapToDto(AgencyContractSaveRequest request)
        {
            return new AgencyContractDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(request.Kontragent)
                    : null,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static AgencyContractResponse MapToResponse(AgencyContractDto dto)
        {
            return new AgencyContractResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static AgencyContractCreated MapToCreatedMessage(AgencyContractSaveRequest request)
        {
            return new AgencyContractCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent)
                    : null,
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static AgencyContractUpdated MapToUpdatedMessage(AgencyContractSaveRequest request)
        {
            return new AgencyContractUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                OutsourceState = request.OutsourceState,
                OperationState = request.OperationState,
            };
        }

        internal static AgencyContractProvideRequired MapToProvideRequired(AgencyContractResponse response)
        {
            return new AgencyContractProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(response.Kontragent),
                ContractBaseId = response.Contract.GetOrThrow().DocumentBaseId,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid
            };
        }

        internal static AgencyContractDeleted MapToDeleted(AgencyContractResponse response, long? newDocumentBaseId)
        {
            return new AgencyContractDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static AgencyContractSaveRequest MapToSaveRequest(AgencyContractImportRequest request)
        {
            return new AgencyContractSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                ContractBaseId = request.ContractBaseId,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                ProvideInAccounting = true,
                IsPaid = true,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static AgencyContractSaveRequest MapToSaveRequest(AgencyContractResponse response)
        {
            return new AgencyContractSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId ?? 0L,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }
    }
}
