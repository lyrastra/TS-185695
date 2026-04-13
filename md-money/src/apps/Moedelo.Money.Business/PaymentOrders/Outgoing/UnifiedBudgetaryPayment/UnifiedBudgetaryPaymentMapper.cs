using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using BudgetaryPeriod = Moedelo.Money.Domain.Operations.BudgetaryPeriod;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    internal static class UnifiedBudgetaryPaymentMapper
    {
        internal static UnifiedBudgetaryPaymentDto MapToDto(UnifiedBudgetaryPaymentSaveRequest request)
        {
            return new UnifiedBudgetaryPaymentDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                IsPaid = request.IsPaid,
                Uin = request.Uin,
                PayerStatus = request.PayerStatus,
                ProvideInAccounting = request.ProvideInAccounting ?? true,
                Recipient = new UnifiedBudgetaryRecipientRequisitesDto
                {
                    Inn = request.Recipient.Inn,
                    Name = request.Recipient.Name,
                    Kpp = request.Recipient.Kpp,
                    SettlementAccount = request.Recipient.SettlementAccount,
                    UnifiedSettlementAccount = request.Recipient.UnifiedSettlementAccount,
                    BankName = request.Recipient.BankName,
                    BankBik = request.Recipient.BankBik,
                    Oktmo = request.Recipient.Oktmo
                },
                SubPayments = request.SubPayments?.Select(sub => new UnifiedBudgetarySubPaymentDto
                {
                    DocumentBaseId = sub.DocumentBaseId,
                    KbkId = sub.KbkId,
                    Sum = sub.Sum,
                    Period = new UnifiedBudgetaryPeriodDto
                    {
                        Type = sub.Period.Type,
                        Number = sub.Period.Number,
                        Year = sub.Period.Year,
                        Date = sub.Period.Date
                    },
                    PatentId = sub.PatentId,
                    TradingObjectId = sub.TradingObjectId
                }).ToArray() ?? Array.Empty<UnifiedBudgetarySubPaymentDto>(),
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
            };
        }

        internal static UnifiedBudgetaryPaymentResponse MapToResponse(UnifiedBudgetaryPaymentDto dto)
        {
            return new UnifiedBudgetaryPaymentResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                KbkId = dto.KbkId,
                IsPaid = dto.IsPaid,
                Uin = dto.Uin,
                ProvideInAccounting = dto.ProvideInAccounting,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
                PayerStatus = dto.PayerStatus,
                SubPayments = dto.SubPayments.Select(MapToResponse).ToArray(),
                Recipient = new UnifiedBudgetaryRecipient
                {
                    Inn = dto.Recipient.Inn,
                    Name = dto.Recipient.Name,
                    Kpp = dto.Recipient.Kpp,
                    SettlementAccount = dto.Recipient.SettlementAccount,
                    UnifiedSettlementAccount = dto.Recipient.UnifiedSettlementAccount,
                    BankName = dto.Recipient.BankName,
                    BankBik = dto.Recipient.BankBik,
                    Oktmo = dto.Recipient.Oktmo
                },
            };
        }

        public static UnifiedBudgetarySubPayment MapToResponse(UnifiedBudgetarySubPaymentDto sub)
        {
            return new UnifiedBudgetarySubPayment
            {
                DocumentBaseId = sub.DocumentBaseId,
                ParentDocumentId = sub.ParentDocumentId,
                Kbk = new SubKbk { Id = sub.KbkId },
                Sum = sub.Sum,
                Period = new BudgetaryPeriod
                {
                    Type = sub.Period.Type,
                    Number = sub.Period.Number,
                    Year = sub.Period.Year,
                    Date = sub.Period.Date
                },
                PatentId = sub.PatentId,
                TradingObjectId = sub.TradingObjectId,
                TaxPostingsInManualMode = sub.TaxPostingType == ProvidePostingType.ByHand
            };
        }

        internal static UnifiedBudgetaryPaymentSaveRequest MapToSaveRequest(UnifiedBudgetaryPaymentResponse response)
        {
            return new UnifiedBudgetaryPaymentSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                IsPaid = response.IsPaid,
                ProvideInAccounting = response.ProvideInAccounting,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                PayerStatus = response.PayerStatus,
                SubPayments = response.SubPayments.Select(MapToSaveRequest).ToArray(),
                Recipient = new UnifiedBudgetaryRecipient
                {
                    Inn = response.Recipient.Inn,
                    Name = response.Recipient.Name,
                    Kpp = response.Recipient.Kpp,
                    SettlementAccount = response.Recipient.SettlementAccount,
                    UnifiedSettlementAccount = response.Recipient.UnifiedSettlementAccount,
                    BankName = response.Recipient.BankName,
                    BankBik = response.Recipient.BankBik,
                    Oktmo = response.Recipient.Oktmo
                }
            };
        }

        private static UnifiedBudgetarySubPaymentSaveModel MapToSaveRequest(UnifiedBudgetarySubPayment sub)
        {
            return new UnifiedBudgetarySubPaymentSaveModel
            {
                DocumentBaseId = sub.DocumentBaseId,
                KbkId = sub.Kbk.Id,
                Sum = sub.Sum,
                Period = new BudgetaryPeriod
                {
                    Type = sub.Period.Type,
                    Number = sub.Period.Number,
                    Year = sub.Period.Year,
                    Date = sub.Period.Date
                },
                PatentId = sub.PatentId,
                TradingObjectId = sub.TradingObjectId,
            };
        }

        internal static UnifiedBudgetaryPaymentCreated MapToCreatedMessage(UnifiedBudgetaryPaymentSaveRequest request)
        {
            return new UnifiedBudgetaryPaymentCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                SubPayments = MapSubPaymentsToKafkaModels(request.SubPayments),
                ProvideInAccounting = request.ProvideInAccounting ?? false,
                IsPaid = request.IsPaid,
                Uin = request.Uin,
                PayerStatus = request.PayerStatus,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static UnifiedBudgetaryPaymentUpdated MapToUpdatedMessage(
            UnifiedBudgetaryPaymentSaveRequest request,
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds)
        {
            return new UnifiedBudgetaryPaymentUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                SubPayments = MapSubPaymentsToKafkaModels(request.SubPayments),
                ProvideInAccounting = request.ProvideInAccounting ?? false,
                IsPaid = request.IsPaid,
                Uin = request.Uin,
                DeletedSubPaymentDocumentIds = deletedSubPaymentDocumentIds,
                PayerStatus = request.PayerStatus,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState
            };
        }

        internal static UnifiedBudgetaryPaymentProvideRequired MapToProvideRequired(UnifiedBudgetaryPaymentResponse response)
        {
            return new UnifiedBudgetaryPaymentProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                SubPayments = response.SubPayments.Select(sub => new Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models.UnifiedBudgetarySubPayment
                {
                    DocumentBaseId = sub.DocumentBaseId,
                    KbkId = sub.Kbk.Id,
                    Period = new Kafka.Abstractions.Models.BudgetaryPeriod
                    {
                        Type = sub.Period.Type,
                        Year = sub.Period.Year,
                        Number = sub.Period.Number,
                        Date = sub.Period.Date
                    },
                    Sum = sub.Sum,
                    TradingObjectId = sub.TradingObjectId,
                    PatentId = sub.PatentId,
                    CurrencyInvoicesLinks = MapCurrencyInvoicesLinks(sub.CurrencyInvoices)
                }).ToArray(),
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid
            };
        }

        internal static UnifiedBudgetaryPaymentDeleted MapToDeleted(UnifiedBudgetaryPaymentResponse response, IReadOnlyCollection<long> deletedSubPaymentDocumentIds, long? newDocumentBaseId)
        {
            return new UnifiedBudgetaryPaymentDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                DeletedSubPaymentDocumentIds = deletedSubPaymentDocumentIds,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static UnifiedBudgetaryPaymentSaveResponse MapToResponse(UnifiedBudgetaryPaymentUpdateResponseDto dto)
        {
            return new UnifiedBudgetaryPaymentSaveResponse
            {
                DeletedSubPaymentDocumentIds = dto.DeletedSubPaymentDocumentIds
            };
        }

        internal static UnifiedBudgetaryPaymentDeleteResponse MapToResponse(UnifiedBudgetaryPaymentDeleteResponseDto dto)
        {
            return new UnifiedBudgetaryPaymentDeleteResponse
            {
                DeletedSubPaymentDocumentIds = dto.DeletedSubPaymentDocumentIds
            };
        }

        internal static UnifiedBudgetaryPaymentSaveRequest MapToSaveRequest(UnifiedBudgetaryPaymentImportRequest request, IReadOnlyCollection<UnifiedBudgetarySubPaymentForDescription> subPayments)
        {
            var saveSubPayments = MapToSaveRequests(subPayments);

            if (IsBadSubPaymentExists(saveSubPayments))
                request.OperationState = OperationState.NoSubPayments;

            return new UnifiedBudgetaryPaymentSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                Recipient = request.Recipient,
                ProvideInAccounting = true,
                IsPaid = true,
                Uin = request.Uin,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                SubPayments = saveSubPayments,
                OperationState = saveSubPayments.Sum(s=> s.Sum) == request.Sum ? request.OperationState : OperationState.NoSubPayments,
                OutsourceState = request.OutsourceState,
                PayerStatus = request.PayerStatus,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        public static UnifiedBudgetarySubPaymentSaveModel[] MapToSaveRequests(this IEnumerable<UnifiedBudgetarySubPaymentForDescription> subPayments)
        {
            return subPayments?.Select(s=> new UnifiedBudgetarySubPaymentSaveModel 
            { 
                KbkId = s.KbkId,
                Sum = s.Sum,
                Period = s.Period,
                PatentId = s.PatentId,
                TradingObjectId = s.TradingObjectId,
            }).ToArray() ?? Array.Empty<UnifiedBudgetarySubPaymentSaveModel>();
        }

        public static bool IsBadSubPaymentExists(this IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveModel> subPayments)
        {
            return subPayments.Any(s =>
            (s.KbkId == 224 && s.PatentId == null) ||
            (s.KbkId == 152 && s.TradingObjectId == null) ||
            (s.Sum == 0) ||
            (s.Period.Type == BudgetaryPeriodType.NoPeriod));
        }

        private static Kafka.Abstractions.Models.DocumentLink[] MapCurrencyInvoicesLinks(IReadOnlyCollection<DocumentLinkSaveRequest> links)
        {
            return links?.Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
            {
                DocumentBaseId = documentLink.DocumentBaseId,
                LinkSum = documentLink.LinkSum
            }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.DocumentLink>();
        }

        private static Kafka.Abstractions.Models.DocumentLink[] MapCurrencyInvoicesLinks(RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> links)
        {
            return links.GetOrThrow().Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
            {
                DocumentBaseId = documentLink.DocumentBaseId,
                LinkSum = documentLink.LinkSum
            }).ToArray();
        }

        private static
            IReadOnlyCollection<Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models.
                UnifiedBudgetarySubPayment> MapSubPaymentsToKafkaModels(
                IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveModel> subPayments)
        {
            return subPayments?.Select(MapSubpaymentToKafkaModel).ToArray() ?? Array
                .Empty<Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models.
                    UnifiedBudgetarySubPayment>();
        }

        private static Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models.UnifiedBudgetarySubPayment
            MapSubpaymentToKafkaModel(UnifiedBudgetarySubPaymentSaveModel subPayment)
        {
            return new Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models.UnifiedBudgetarySubPayment
            {
                DocumentBaseId = subPayment.DocumentBaseId,
                KbkId = subPayment.KbkId,
                Period = new Kafka.Abstractions.Models.BudgetaryPeriod
                {
                    Type = subPayment.Period.Type,
                    Year = subPayment.Period.Year,
                    Number = subPayment.Period.Number,
                    Date = subPayment.Period.Date
                },
                Sum = subPayment.Sum,
                TradingObjectId = subPayment.TradingObjectId,
                PatentId = subPayment.PatentId,
                IsManualTaxPostings = subPayment.TaxPostings?.ProvidePostingType == ProvidePostingType.ByHand,
                CurrencyInvoicesLinks = MapCurrencyInvoicesLinks(subPayment.CurrencyInvoices)
            };
        }
    }
}
