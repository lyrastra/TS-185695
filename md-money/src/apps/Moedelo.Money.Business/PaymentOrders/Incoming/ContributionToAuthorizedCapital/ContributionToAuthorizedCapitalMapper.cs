using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    internal static class ContributionToAuthorizedCapitalMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(ContributionToAuthorizedCapitalSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static ContributionToAuthorizedCapitalDto MapToDto(ContributionToAuthorizedCapitalSaveRequest request)
        {
            return new ContributionToAuthorizedCapitalDto
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
                PostingsAndTaxMode = Enums.ProvidePostingType.Auto,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static ContributionToAuthorizedCapitalSaveRequest MapToSaveRequest(ContributionToAuthorizedCapitalResponse response)
        {
            return new ContributionToAuthorizedCapitalSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static ContributionToAuthorizedCapitalSaveRequest MapToSaveRequest(ContributionToAuthorizedCapitalImportRequest request)
        {
            return new ContributionToAuthorizedCapitalSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                ProvideInAccounting = true,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static ContributionToAuthorizedCapitalResponse MapToResponse(ContributionToAuthorizedCapitalDto dto)
        {
            return new ContributionToAuthorizedCapitalResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static ContributionToAuthorizedCapitalCreatedMessage MapToCreatedMessage(ContributionToAuthorizedCapitalSaveRequest request)
        {
            return new ContributionToAuthorizedCapitalCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent)
                    : null,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static ContributionToAuthorizedCapitalUpdatedMessage MapToUpdatedMessage(ContributionToAuthorizedCapitalSaveRequest request)
        {
            return new ContributionToAuthorizedCapitalUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static ContributionToAuthorizedCapitalDeletedMessage MapToDeletedMessage(ContributionToAuthorizedCapitalResponse response, long? newDocumentBaseId)
        {
            return new ContributionToAuthorizedCapitalDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
