using System;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    internal static class DeductionMapper
    {
        internal static DeductionSaveRequest MapToSaveRequest(DeductionImportRequest request)
        {
            return new DeductionSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.Contractor,
                ContractBaseId = request.ContractBaseId,
                ProvideInAccounting = true,
                IsPaid = true,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                PayerStatus = MapBudgetaryPayerStatus(request.PayerStatus),
                Kbk = request.Kbk,
                Oktmo = request.Oktmo,
                Uin = request.Uin,
                DeductionWorkerDocumentNumber = request.DeductionWorkerDocumentNumber,
                PaymentPriority = request.PaymentPriority,
                IsBudgetaryDebt = request.IsBudgetaryDebt,
                DeductionWorkerId = request.DeductionWorkerId,
                DeductionWorkerInn = request.DeductionWorkerInn,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static DeductionSaveRequest MapToSaveRequest(DeductionResponse response)
        {
            return new DeductionSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = response.Contractor,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                Kbk = response.Kbk,
                Oktmo = response.Oktmo,
                Uin = response.Uin,
                DeductionWorkerDocumentNumber = response.DeductionWorkerDocumentNumber,
                PaymentPriority = response.PaymentPriority,
                PayerStatus = response.PayerStatus,
                IsBudgetaryDebt = response.IsBudgetaryDebt,
                DeductionWorkerId = response.DeductionWorkerId,
                DeductionWorkerInn = response.DeductionWorkerInn,
                AccountingPosting = new DeductionCustomAccPosting(),
            };
        }

        internal static AccPosting[] MapToPostings(DeductionCustomAccPosting posting)
        {
            return new[]
            {
                new AccPosting
                {
                    Date = posting.Date,
                    Sum = posting.Sum,
                    DebitCode = posting.DebitCode,
                    DebitSubconto = posting.DebitSubconto,
                    CreditCode = posting.CreditCode,
                    CreditSubconto = new[] { new Subconto { Id = posting.CreditSubconto } },
                    Description = posting.Description
                }
            };
        }
        
        internal static DeductionDto MapToDto(DeductionSaveRequest request)
        {
            return new DeductionDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentRequisitesToDto(request.Contractor)
                    : null,
                Description = request.Description,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                Kbk = request.Kbk,
                Oktmo = request.Oktmo,
                Uin = request.Uin,
                DeductionWorkerDocumentNumber = request.DeductionWorkerDocumentNumber,
                DeductionWorkerId = request.DeductionWorkerId,
                DeductionWorkerInn = request.DeductionWorkerInn,
                PaymentPriority = request.PaymentPriority,
                IsBudgetaryDebt = request.IsBudgetaryDebt,
                PayerStatus = request.PayerStatus,
                ProvideInAccounting = request.ProvideInAccounting,
            };
        }

        internal static DeductionResponse MapToResponse(DeductionDto dto)
        {
            return new DeductionResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentRequisites(dto.Contractor),
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                Kbk = dto.Kbk,
                Oktmo = dto.Oktmo,
                Uin = dto.Uin,
                DeductionWorkerDocumentNumber = dto.DeductionWorkerDocumentNumber,
                PaymentPriority = dto.PaymentPriority,
                DeductionWorkerId = dto.DeductionWorkerId,
                DeductionWorkerInn = dto.DeductionWorkerInn,
                IsBudgetaryDebt = dto.IsBudgetaryDebt,
                PayerStatus = dto.PayerStatus,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static DeductionCreated MapToCreatedMessage(DeductionSaveRequest request)
        {
            return new DeductionCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Contractor)
                    : null,
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                Kbk = request.Kbk,
                Oktmo = request.Oktmo,
                Uin = request.Uin,
                DeductionWorkerDocumentNumber = request.DeductionWorkerDocumentNumber,
                PaymentPriority = request.PaymentPriority,
                IsBudgetaryDebt = request.IsBudgetaryDebt,
                DeductionWorkerId = request.DeductionWorkerId,
                DeductionWorkerInn = request.DeductionWorkerInn,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static DeductionUpdated MapToUpdatedMessage(DeductionSaveRequest request)
        {
            return new DeductionUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Contractor),
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                Kbk = request.Kbk,
                Oktmo = request.Oktmo,
                Uin = request.Uin,
                DeductionWorkerDocumentNumber = request.DeductionWorkerDocumentNumber,
                PaymentPriority = request.PaymentPriority,
                IsBudgetaryDebt = request.IsBudgetaryDebt,
                DeductionWorkerId = request.DeductionWorkerId,
                DeductionWorkerInn = request.DeductionWorkerInn,
            };
        }

        internal static DeductionProvideRequired MapToProvideRequired(DeductionResponse response)
        {
            return new DeductionProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(response.Contractor),
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                Kbk = response.Kbk,
                Oktmo = response.Oktmo,
                Uin = response.Uin,
                DeductionWorkerDocumentNumber = response.DeductionWorkerDocumentNumber,
                PaymentPriority = response.PaymentPriority,
                IsBudgetaryDebt = response.IsBudgetaryDebt,
                DeductionWorkerId = response.DeductionWorkerId,
                DeductionWorkerInn = response.DeductionWorkerInn
            };
        }

        internal static DeductionDeleted MapToDeleted(DeductionResponse response, long? newDocumentBaseId)
        {
            return new DeductionDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Contractor?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        public static DeductionAccPostingRequest MapToAccPostingRequest(DeductionResponse response)
        {
            return new DeductionAccPostingRequest
            {
                SettlementAccountId = response.SettlementAccountId,
                Date = response.Date, 
                Sum = response.Sum, 
                PayerStatus = response.PayerStatus,
                Contractor = response.Contractor,
                Kbk = response.Kbk
            };
        }

        public static DeductionAccPostingRequest MapToAccPostingRequest(DeductionImportRequest request)
        {
            return new DeductionAccPostingRequest
            {
                SettlementAccountId = request.SettlementAccountId,
                Date = request.Date, 
                Sum = request.Sum, 
                PayerStatus = MapBudgetaryPayerStatus(request.PayerStatus),
                Contractor = request.Contractor, 
                Kbk = request.Kbk
            };
        }

        public static DeductionAccPostingRequest MapToAccPostingRequest(DeductionSaveRequest saveRequest)
        {
            return new DeductionAccPostingRequest
            {
                SettlementAccountId = saveRequest.SettlementAccountId,
                Date = saveRequest.Date,
                Sum = saveRequest.Sum,
                PayerStatus = saveRequest.PayerStatus,
                Contractor = saveRequest.Contractor,
                Kbk = saveRequest.Kbk
            };
        }

        private static BudgetaryPayerStatus MapBudgetaryPayerStatus(string payerStatus)
        {
            return Enum.TryParse(payerStatus, out BudgetaryPayerStatus status)
                ? status
                : BudgetaryPayerStatus.None;
        }
    }
}
