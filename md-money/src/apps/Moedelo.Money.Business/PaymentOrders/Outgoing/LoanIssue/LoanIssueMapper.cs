using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue
{
    internal static class LoanIssueMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(LoanIssueSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static LoanIssueDto MapToDto(LoanIssueSaveRequest request)
        {
            return new LoanIssueDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(request.Kontragent)
                    : null,
                IsLongTermLoan = request.IsLongTermLoan,
                Description = request.Description,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static LoanIssueResponse MapToResponse(LoanIssueDto dto)
        {
            return new LoanIssueResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                IsLongTermLoan = dto.IsLongTermLoan,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static LoanIssueSaveRequest MapToSaveRequest(LoanIssueResponse response)
        {
            return new LoanIssueSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId ?? default,
                IsLongTermLoan = response.IsLongTermLoan,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                IsPaid = response.IsPaid,
                TaxPostings = new TaxPostingsData(),
                OutsourceState = response.OutsourceState,
            };
        }

        internal static LoanIssueSaveRequest MapToSaveRequest(LoanIssueImportRequest request)
        {
            return new LoanIssueSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                ContractBaseId = request.ContractBaseId,
                IsLongTermLoan = request.IsLongTermLoan,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                ProvideInAccounting = true,
                IsPaid = true,
                TaxPostings = new TaxPostingsData(),
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static LoanIssueCreated MapToCreatedMessage(LoanIssueSaveRequest request)
        {
            return new LoanIssueCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent)
                    : null,
                ContractBaseId = request.ContractBaseId,
                Description = request.Description,
                Sum = request.Sum,
                IsLongTermLoan = request.IsLongTermLoan,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static LoanIssueUpdated MapToUpdatedMessage(LoanIssueSaveRequest request)
        {
            return new LoanIssueUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
                ContractBaseId = request.ContractBaseId,
                Description = request.Description,
                Sum = request.Sum,
                IsLongTermLoan = request.IsLongTermLoan,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                IsPaid = request.IsPaid,
                OutsourceState = request.OutsourceState,
                OperationState = request.OperationState,
            };
        }

        internal static LoanIssueProvideRequired MapToProvideRequired(LoanIssueResponse response)
        {
            return new LoanIssueProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(response.Kontragent),
                ContractBaseId = response.Contract.GetOrThrow().DocumentBaseId,
                Description = response.Description,
                Sum = response.Sum,
                IsLongTermLoan = response.IsLongTermLoan,
                ProvideInAccounting = response.ProvideInAccounting,
                IsManualTaxPostings = response.TaxPostingsInManualMode,
                IsPaid = response.IsPaid
            };
        }

        internal static LoanIssueDeleted MapToDeleted(LoanIssueResponse response, long? newDocumentBaseId)
        {
            return new LoanIssueDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            LoanIssueSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                Description = request.Description,
                Postings = request.TaxPostings,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                DocumentBaseId = request.DocumentBaseId
            };
        }
    }
}
