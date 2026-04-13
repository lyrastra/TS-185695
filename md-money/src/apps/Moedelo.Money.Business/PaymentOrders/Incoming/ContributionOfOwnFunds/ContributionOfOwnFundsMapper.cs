using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    internal static class ContributionOfOwnFundsMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(ContributionOfOwnFundsSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static ContributionOfOwnFundsDto MapToDto(ContributionOfOwnFundsSaveRequest request)
        {
            return new ContributionOfOwnFundsDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static ContributionOfOwnFundsSaveRequest MapToSaveRequest(ContributionOfOwnFundsResponse response)
        {
            return new ContributionOfOwnFundsSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static ContributionOfOwnFundsSaveRequest MapToSaveRequest(ContributionOfOwnFundsImportRequest request)
        {
            return new ContributionOfOwnFundsSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                DuplicateId = request.DuplicateId,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static ContributionOfOwnFundsResponse MapToResponse(ContributionOfOwnFundsDto dto)
        {
            return new ContributionOfOwnFundsResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState
            };
        }

        internal static ContributionOfOwnFundsCreatedMessage MapToCreatedMessage(ContributionOfOwnFundsSaveRequest request)
        {
            return new ContributionOfOwnFundsCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                Sum = request.Sum,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static ContributionOfOwnFundsUpdatedMessage MapToUpdatedMessage(ContributionOfOwnFundsSaveRequest request)
        {
            return new ContributionOfOwnFundsUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                Sum = request.Sum,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState
            };
        }

        internal static ContributionOfOwnFundsDeletedMessage MapToDeletedMessage(ContributionOfOwnFundsResponse response, long? newDocumentBaseId)
        {
            return new ContributionOfOwnFundsDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}
