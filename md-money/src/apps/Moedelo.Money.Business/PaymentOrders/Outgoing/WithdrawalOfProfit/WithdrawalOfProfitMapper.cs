using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    internal static class WithdrawalOfProfitMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(WithdrawalOfProfitSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static WithdrawalOfProfitDto MapToDto(WithdrawalOfProfitSaveRequest request)
        {
            return new WithdrawalOfProfitDto
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
                IsPaid = request.IsPaid,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                IsIgnoreNumber = request.IsIgnoreNumber
            };
        }

        internal static WithdrawalOfProfitSaveRequest MapToSaveRequest(WithdrawalOfProfitResponse response)
        {
            return new WithdrawalOfProfitSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                Description = response.Description,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                IsPaid = response.IsPaid,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static WithdrawalOfProfitSaveRequest MapToSaveRequest(WithdrawalOfProfitImportRequest request)
        {
            return new WithdrawalOfProfitSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                IsPaid = true,
                IsIgnoreNumber = request.IsIgnoreNumber,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static WithdrawalOfProfitResponse MapToResponse(WithdrawalOfProfitDto dto)
        {
            return new WithdrawalOfProfitResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                IsPaid = dto.IsPaid,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId)
            };
        }

        internal static WithdrawalOfProfitCreated MapToCreatedMessage(WithdrawalOfProfitSaveRequest request)
        {
            return new WithdrawalOfProfitCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent)
                    : null,
                Sum = request.Sum,
                Description = request.Description,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static WithdrawalOfProfitUpdated MapToUpdatedMessage(WithdrawalOfProfitSaveRequest request)
        {
            return new WithdrawalOfProfitUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
                Sum = request.Sum,
                Description = request.Description,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static WithdrawalOfProfitProvideRequired MapToProvideRequired(WithdrawalOfProfitResponse response)
        {
            return new WithdrawalOfProfitProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(response.Kontragent),
                Sum = response.Sum,
                Description = response.Description,
                IsPaid = response.IsPaid
            };
        }

        internal static WithdrawalOfProfitDeleted MapToDeleted(WithdrawalOfProfitResponse response, long? newDocumentBaseId)
        {
            return new WithdrawalOfProfitDeleted
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
