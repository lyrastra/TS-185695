using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    internal static class RefundToCustomerMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(RefundToCustomerSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static RefundToCustomerDto MapToDto(RefundToCustomerSaveRequest request)
        {
            return new RefundToCustomerDto
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
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                IsMainContractor = request.IsMainContractor,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static RefundToCustomerResponse MapToResponse(RefundToCustomerDto dto)
        {
            return new RefundToCustomerResponse
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
                IsMainContractor = dto.IsMainContractor,
                ProvideInAccounting = dto.ProvideInAccounting,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                IsPaid = dto.IsPaid,
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static RefundToCustomerSaveRequest MapToSaveRequest(RefundToCustomerResponse response)
        {
            return new RefundToCustomerSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Kontragent = response.Kontragent,
                Description = response.Description,
                IncludeNds = response.IncludeNds,
                NdsType = response.NdsType,
                NdsSum = response.NdsSum,
                IsMainContractor = response.IsMainContractor,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static RefundToCustomerSaveRequest MapToSaveRequest(RefundToCustomerImportRequest request)
        {
            return new RefundToCustomerSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Kontragent = request.Kontragent,
                Description = request.Description,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                IsMainContractor = true,
                ContractBaseId = request.ContractBaseId,
                ProvideInAccounting = true,
                IsPaid = true,
                TaxationSystemType = request.TaxationSystemType,
                TaxPostings = new TaxPostingsData(),
                PatentId = request.PatentId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static RefundToCustomerCreated MapToCreatedMessage(RefundToCustomerSaveRequest request)
        {
            return new RefundToCustomerCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent)
                    : null,
                IsMainContractor = request.IsMainContractor,
                ContractBaseId = request.ContractBaseId,
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                IsPaid = request.IsPaid,
                TaxationSystemType = request.TaxationSystemType.Value,
                PatentId = request.PatentId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static RefundToCustomerUpdated MapToUpdatedMessage(RefundToCustomerSaveRequest request)
        {
            return new RefundToCustomerUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(request.Kontragent),
                IsMainContractor = request.IsMainContractor,
                ContractBaseId = request.ContractBaseId,
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                IsPaid = request.IsPaid,
                TaxationSystemType = request.TaxationSystemType.Value,
                PatentId = request.PatentId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static RefundToCustomerProvideRequired MapToProvideRequired(RefundToCustomerResponse response)
        {
            return new RefundToCustomerProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapKontragentWithRequisitesToKafka(response.Kontragent),
                IsMainContractor = response.IsMainContractor,
                ContractBaseId = response.Contract.GetOrThrow()?.DocumentBaseId,
                // На данный момент (февраль 2021) ui денежной операции не знает про связи с возвратом.
                // Однако при связывании с последним нужно поменять БУ (достаточно признака наличия связи).
                HasLinkedRetailRefund = response.RetailRefund.GetOrThrow() != null,
                Nds = response.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = response.NdsSum,
                        NdsType = response.NdsType
                    }
                    : null,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsManualTaxPostings = response.TaxPostingsInManualMode,
                IsPaid = response.IsPaid,
                TaxationSystemType = response.TaxationSystemType.Value,
                PatentId = response.PatentId
            };
        }

        internal static RefundToCustomerDeleted MapToDeleted(RefundToCustomerResponse response, long? newDocumentBaseId)
        {
            return new RefundToCustomerDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            RefundToCustomerSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                Description = request.Description,
                Postings = request.TaxPostings,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                DocumentBaseId = request.DocumentBaseId,
                TaxationSystemType = request.TaxationSystemType
            };
        }
    }
}
