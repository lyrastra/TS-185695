using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn
{
    internal static class LoanReturnMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(LoanReturnSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static LoanReturnDto MapToDto(LoanReturnSaveRequest request)
        {
            return new LoanReturnDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                LoanInterestSum = request.LoanInterestSum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(request.Kontragent)
                    : null,
                IsLongTermLoan = request.IsLongTermLoan,
                Description = request.Description,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                ProvideInAccounting = request.ProvideInAccounting,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static LoanReturnSaveRequest MapToSaveRequest(LoanReturnResponse response)
        {
            return new LoanReturnSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                LoanInterestSum = response.LoanInterestSum,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId ?? default,
                IsLongTermLoan = response.IsLongTermLoan,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static LoanReturnSaveRequest MapToSaveRequest(LoanReturnImportRequest request)
        {
            return new LoanReturnSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                LoanInterestSum = request.LoanInterestSum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                ContractBaseId = request.ContractBaseId,
                IsLongTermLoan = request.IsLongTermLoan,
                Description = request.Description,
                ProvideInAccounting = true,
                TaxPostings = new TaxPostingsData(),
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static LoanReturnResponse MapToResponse(LoanReturnDto dto)
        {
            return new LoanReturnResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                LoanInterestSum = dto.LoanInterestSum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapKontragentRequisites(dto.Kontragent),
                Description = dto.Description,
                IsLongTermLoan = dto.IsLongTermLoan,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                ProvideInAccounting = dto.ProvideInAccounting,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static LoanReturnCreated MapToCreatedMessage(LoanReturnSaveRequest request)
        {
            return new LoanReturnCreated
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
                LoanInterestSum = request.LoanInterestSum,
                IsLongTermLoan = request.IsLongTermLoan,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static LoanReturnUpdated MapToUpdatedMessage(LoanReturnSaveRequest request)
        {
            return new LoanReturnUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
                ContractBaseId = request.ContractBaseId,
                Description = request.Description,
                Sum = request.Sum,
                LoanInterestSum = request.LoanInterestSum,
                IsLongTermLoan = request.IsLongTermLoan,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        public static LoanReturnProvideRequired MapToProvideRequired(LoanReturnResponse payment)
        {
            return new LoanReturnProvideRequired
            {
                DocumentBaseId = payment.DocumentBaseId,
                Date = payment.Date,
                Number = payment.Number,
                SettlementAccountId = payment.SettlementAccountId,
                Contractor = payment.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(payment.Kontragent)
                    : null,
                ContractBaseId = payment.Contract.Data.DocumentBaseId,
                Description = payment.Description,
                Sum = payment.Sum,
                LoanInterestSum = payment.LoanInterestSum,
                IsLongTermLoan = payment.IsLongTermLoan,
                ProvideInAccounting = payment.ProvideInAccounting,
                IsManualTaxPostings = payment.TaxPostingsInManualMode
            };
        }

        public static LoanReturnDeleted MapToDeleted(LoanReturnResponse payment, long? newDocumentBaseId)
        {
            return new LoanReturnDeleted
            {
                DocumentBaseId = payment.DocumentBaseId,
                Date = payment.Date,
                Number = payment.Number,
                KontragentId = payment.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            LoanReturnSaveRequest request)
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
