using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using BudgetaryPeriod = Moedelo.Money.Domain.Operations.BudgetaryPeriod;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    internal static class BudgetaryPaymentMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(BudgetaryPaymentSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static BudgetaryPaymentDto MapToDto(BudgetaryPaymentSaveRequest request)
        {
            return new BudgetaryPaymentDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                AccountCode = request.AccountCode,
                PaymentType = request.PaymentType,
                KbkPaymentType = request.KbkPaymentType,
                KbkId = request.KbkId,
                KbkNumber = request.KbkNumber,
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                Period = new BudgetaryPeriodDto
                {
                    Type = request.Period.Type,
                    Date = request.Period.Date,
                    Number = request.Period.Number,
                    Year = request.Period.Year
                },
                PayerStatus = request.PayerStatus,
                PaymentBase = request.PaymentBase,
                DocumentDate = request.DocumentDate,
                DocumentNumber = request.DocumentNumber,
                Uin = request.Uin,
                Recipient = MapToDto(request.Recipient),
                ProvideInAccounting = request.ProvideInAccounting,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                IsPaid = request.IsPaid,
                TradingObjectId = request.TradingObjectId,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static BudgetaryPaymentResponse MapToResponse(BudgetaryPaymentDto dto)
        {
            return new BudgetaryPaymentResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                AccountCode = dto.AccountCode,
                PaymentType = dto.PaymentType,
                KbkPaymentType = dto.KbkPaymentType,
                KbkId = dto.KbkId,
                KbkNumber = dto.KbkNumber,
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                Period = new BudgetaryPeriod
                {
                    Type = dto.Period.Type,
                    Date = dto.Period.Date,
                    Number = dto.Period.Number,
                    Year = dto.Period.Year
                },
                PayerStatus = dto.PayerStatus,
                PaymentBase = dto.PaymentBase,
                DocumentDate = dto.DocumentDate,
                DocumentNumber = dto.DocumentNumber,
                Uin = dto.Uin,
                Recipient = MapToRecipient(dto.Recipient),
                ProvideInAccounting = dto.ProvideInAccounting,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                IsPaid = dto.IsPaid,
                TradingObjectId = dto.TradingObjectId,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static BudgetaryPaymentSaveRequest MapToSaveRequest(BudgetaryPaymentResponse response)
        {
            return new BudgetaryPaymentSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                AccountCode = response.AccountCode,
                PaymentType = response.PaymentType,
                KbkPaymentType = response.KbkPaymentType,
                KbkId = response.KbkId,
                KbkNumber = response.KbkNumber,
                Period = new BudgetaryPeriod
                {
                    Type = response.Period.Type,
                    Date = response.Period.Date,
                    Number = response.Period.Number,
                    Year = response.Period.Year
                },
                PayerStatus = response.PayerStatus,
                PaymentBase = response.PaymentBase,
                DocumentDate = response.DocumentDate,
                DocumentNumber = response.DocumentNumber,
                Uin = response.Uin,
                Recipient = response.Recipient,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                TradingObjectId = response.TradingObjectId,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static BudgetaryPaymentSaveRequest MapToSaveRequest(BudgetaryPaymentImportRequest request)
        {
            return new BudgetaryPaymentSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                AccountCode = request.AccountCode,
                PaymentType = request.PaymentType,
                KbkPaymentType = request.KbkPaymentType,
                KbkId = request.KbkId,
                KbkNumber = request.KbkNumber,
                Period = request.Period,
                PayerStatus = request.PayerStatus,
                PaymentBase = request.PaymentBase,
                DocumentDate = request.DocumentDate,
                DocumentNumber = request.DocumentNumber,
                Uin = request.Uin,
                Recipient = request.Recipient,
                ProvideInAccounting = true,
                IsPaid = true,
                TaxPostings = new TaxPostingsData(),
                TradingObjectId = request.TradingObjectId,
                PatentId = request.PatentId,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        internal static BudgetaryPaymentCreated MapToCreatedMessage(BudgetaryPaymentSaveRequest request)
        {
            return new BudgetaryPaymentCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                AccountCode = request.AccountCode,
                KbkId = request.KbkId,
                KbkNumber = request.KbkNumber,
                KbkPaymentType = request.KbkPaymentType,
                Period = new Kafka.Abstractions.Models.BudgetaryPeriod
                {
                    Type = request.Period.Type,
                    Number = request.Period.Number,
                    Year = request.Period.Year,
                    Date = request.Period.Date
                },
                TradingObjectId = request.TradingObjectId,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                IsPaid = request.IsPaid,
                CurrencyInvoicesLinks = MapCurrencyInvoicesLinks(request.CurrencyInvoices),
                OperationState = request.OperationState,
                PayerStatus = request.PayerStatus,
                PaymentBase = request.PaymentBase,
                DocumentDate = request.DocumentDate,
                DocumentNumber = request.DocumentNumber,
                Uin = request.Uin, // это Код рядом с номером и датой документа (22)
                Recipient = request.Recipient.MapRecipient(),
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        private static Kafka.Abstractions.Models.DocumentLink[] MapCurrencyInvoicesLinks(IReadOnlyCollection<DocumentLinkSaveRequest> links)
        {
            return links?.Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
            {
                DocumentBaseId = documentLink.DocumentBaseId,
                LinkSum = documentLink.LinkSum
            }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.DocumentLink>();
        }

        internal static BudgetaryPaymentUpdated MapToUpdatedMessage(BudgetaryPaymentSaveRequest request)
        {
            return new BudgetaryPaymentUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Sum = request.Sum,
                Description = request.Description,
                AccountCode = request.AccountCode,
                KbkId = request.KbkId,
                KbkNumber = request.KbkNumber,
                KbkPaymentType = request.KbkPaymentType,
                Period = new Kafka.Abstractions.Models.BudgetaryPeriod
                {
                    Type = request.Period.Type,
                    Number = request.Period.Number,
                    Year = request.Period.Year,
                    Date = request.Period.Date
                },
                TradingObjectId = request.TradingObjectId,
                ProvideInAccounting = request.ProvideInAccounting,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                IsPaid = request.IsPaid,
                CurrencyInvoicesLinks = MapCurrencyInvoicesLinks(request.CurrencyInvoices),
                PayerStatus = request.PayerStatus,
                PaymentBase = request.PaymentBase,
                DocumentDate = request.DocumentDate,
                DocumentNumber = request.DocumentNumber,
                Uin = request.Uin, // это Код рядом с номером и датой документа (22)
                Recipient = request.Recipient.MapRecipient(),
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        private static BudgetaryPaymentRecipient MapRecipient(this BudgetaryRecipient recipient)
        {
            return new()
            {
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                Name = recipient.Name,
                Okato = recipient.Okato,
                Oktmo = recipient.Oktmo,
                BankBik = recipient.BankBik,
                BankName = recipient.BankName,
                SettlementAccount = recipient.SettlementAccount,
                BankCorrespondentAccount = recipient.UnifiedSettlementAccount
            };
        }

        internal static BudgetaryPaymentProvideRequired MapToProvideRequired(BudgetaryPaymentResponse response)
        {
            return new BudgetaryPaymentProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                AccountCode = response.AccountCode,
                KbkId = response.KbkId,
                KbkNumber = response.KbkNumber,
                KbkPaymentType = response.KbkPaymentType,
                Period = new Kafka.Abstractions.Models.BudgetaryPeriod
                {
                    Type = response.Period.Type,
                    Number = response.Period.Number,
                    Year = response.Period.Year,
                    Date = response.Period.Date
                },
                TradingObjectId = response.TradingObjectId,
                ProvideInAccounting = response.ProvideInAccounting,
                IsManualTaxPostings = response.TaxPostingsInManualMode,
                IsPaid = response.IsPaid,
                CurrencyInvoicesLinks = MapCurrencyInvoicesLinks(response.CurrencyInvoices)
            };
        }

        internal static BudgetaryPaymentDeleted MapToDeleted(BudgetaryPaymentResponse response, long? newDocumentBaseId)
        {
            return new BudgetaryPaymentDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        private static BudgetaryRecipientRequisitesDto MapToDto(BudgetaryRecipient recipient)
        {
            return new BudgetaryRecipientRequisitesDto
            {
                Name = recipient.Name,
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                SettlementAccount = recipient.SettlementAccount,
                BankName = recipient.BankName,
                BankCorrespondentAccount = recipient.UnifiedSettlementAccount,
                BankBik = recipient.BankBik,
                Okato = recipient.Okato,
                Oktmo = recipient.Oktmo
            };
        }

        private static BudgetaryRecipient MapToRecipient(BudgetaryRecipientRequisitesDto recipient)
        {
            return new BudgetaryRecipient
            {
                Name = recipient.Name,
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                SettlementAccount = recipient.SettlementAccount,
                BankName = recipient.BankName,
                UnifiedSettlementAccount = recipient.BankCorrespondentAccount,
                BankBik = recipient.BankBik,
                Okato = recipient.Okato,
                Oktmo = recipient.Oktmo
            };
        }

        internal static BudgetaryPaymentReason[] MapToDomain(IReadOnlyCollection<BudgetaryPaymentReasonDto> reasons)
        {
            return reasons.Select(x =>
                new BudgetaryPaymentReason
                {
                    Designation = x.Designation,
                    Description = x.Description,
                    Code = x.Code
                }).ToArray();
        }

        private static Kafka.Abstractions.Models.DocumentLink[] MapCurrencyInvoicesLinks(RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> links)
        {
            return links.GetOrThrow().Select(documentLink => new Kafka.Abstractions.Models.DocumentLink
            {
                DocumentBaseId = documentLink.DocumentBaseId,
                LinkSum = documentLink.LinkSum
            }).ToArray();
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            BudgetaryPaymentSaveRequest request)
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
