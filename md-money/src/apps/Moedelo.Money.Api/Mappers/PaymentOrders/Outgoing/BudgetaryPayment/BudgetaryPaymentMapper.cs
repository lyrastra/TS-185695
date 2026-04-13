using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Commands;
using Moedelo.TaxPostings.Enums;
using System.Text.RegularExpressions;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.TaxPostings;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment
{
    public static class BudgetaryPaymentMapper
    {
        private static readonly DateTime reasonChangeDate = new DateTime(2021, 10, 1);
        public static BudgetaryPaymentResponseDto Map(BudgetaryPaymentResponse response)
        {
            return new BudgetaryPaymentResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Description = response.Description,
                AccountCode = response.AccountCode,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                KbkPaymentType = response.KbkPaymentType,
                Kbk = new BudgetaryKbkResponseDto
                {
                    Id = response.KbkId,
                    Number = response.KbkNumber
                },
                Period = BudgetaryPeriodMapper.MapToDto(response.Period),
                PayerStatus = response.PayerStatus,
                PaymentBase = response.PaymentBase,
                DocumentDate = response.DocumentDate,
                DocumentNumber = response.DocumentNumber,
                Uin = response.Uin,
                Recipient = BudgetaryRecipientMapper.MapToDto(response.Recipient),
                ProvideInAccounting = response.ProvideInAccounting,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                TradingObjectId = response.TradingObjectId,
                CurrencyInvoices = MapCurrencyInvoices(response.CurrencyInvoices),
                IsFromImport = response.IsFromImport
            };
        }
        
        private static RemoteServiceResponseDto<CurrencyInvoiceResponseDto[]> MapCurrencyInvoices(RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> response)
        {
            return new RemoteServiceResponseDto<CurrencyInvoiceResponseDto[]>
            {
                Data = response.Data?.Select(link =>
                    new CurrencyInvoiceResponseDto
                    {
                        DocumentBaseId = link.DocumentBaseId,
                        Date = link.DocumentDate,
                        Number = link.DocumentNumber,
                        LinkSum = link.LinkSum
                    }).ToArray(),
                Status = response.Status
            };
        }

        public static BudgetaryPaymentSaveRequest Map(BudgetaryPaymentSaveDto dto)
        {
            return new BudgetaryPaymentSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                AccountCode = dto.AccountCode,
                KbkPaymentType = dto.KbkPaymentType,
                KbkId = dto.Kbk.Id,
                KbkNumber = dto.Kbk.Number,
                Period = BudgetaryPeriodMapper.MapToDomain(dto.Period),
                PayerStatus = dto.PayerStatus,
                PaymentBase = dto.PaymentBase,
                DocumentDate = GetDocumentDate(dto),
                DocumentNumber = string.IsNullOrWhiteSpace(dto.DocumentNumber)
                    ? "0"
                    : Regex.Replace(dto.DocumentNumber, @"\s+", " ").Trim(),
                Uin = dto.Uin,
                Recipient = BudgetaryRecipientMapper.MapToDomain(dto.Recipient),
                Description = dto.Description,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                IsPaid = dto.IsPaid,
                TradingObjectId = dto.AccountCode == BudgetaryAccountCodes.TradingFees ? dto.TradingObjectId : null,
                PaymentType = dto.PaymentType ?? (dto.AccountCode.IsSocialInsurance() ? BudgetaryPaymentType.Other : BudgetaryPaymentType.None),
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                CurrencyInvoices = LinkedDocumentsMapper.MapDocumentLinks(dto.CurrencyInvoices),
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static BudgetaryPaymentImportRequest Map(ImportBudgetaryPayment commandData)
        {
            return new BudgetaryPaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                AccountCode = commandData.AccountCode,
                KbkPaymentType = commandData.KbkPaymentType,
                KbkId = commandData.KbkId,
                KbkNumber = commandData.KbkNumber,
                Period = BudgetaryPeriodMapper.MapToDomain(commandData.Period),
                PayerStatus = commandData.PayerStatus,
                PaymentBase = commandData.PaymentBase,
                DocumentDate = commandData.DocumentDate,
                DocumentNumber = commandData.DocumentNumber,
                Uin = commandData.Uin,
                Recipient = BudgetaryRecipientMapper.MapToDomain(commandData.Recipient),
                Description = commandData.Description,
                PaymentType = commandData.AccountCode.IsSocialInsurance() ? BudgetaryPaymentType.Other : BudgetaryPaymentType.None,
                TradingObjectId = commandData.AccountCode == BudgetaryAccountCodes.TradingFees ? commandData.TradingObjectId : null,
                PatentId = commandData.PatentId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static BudgetaryPaymentImportRequest Map(ImportDuplicateBudgetaryPayment commandData)
        {
            return new BudgetaryPaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                AccountCode = commandData.AccountCode,
                KbkPaymentType = commandData.KbkPaymentType,
                KbkId = commandData.KbkId,
                KbkNumber = commandData.KbkNumber,
                Period = BudgetaryPeriodMapper.MapToDomain(commandData.Period),
                PayerStatus = commandData.PayerStatus,
                PaymentBase = commandData.PaymentBase,
                DocumentDate = commandData.DocumentDate,
                DocumentNumber = commandData.DocumentNumber,
                Uin = commandData.Uin,
                Recipient = BudgetaryRecipientMapper.MapToDomain(commandData.Recipient),
                Description = commandData.Description,
                PaymentType = commandData.AccountCode.IsSocialInsurance() ? BudgetaryPaymentType.Other : BudgetaryPaymentType.None,
                TradingObjectId = commandData.AccountCode == BudgetaryAccountCodes.TradingFees ? commandData.TradingObjectId : null,
                PatentId = commandData.PatentId,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Duplicate,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(BudgetaryPaymentResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Recipient.Inn,
                PayeeSettlementAccount = response.Recipient.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        internal static BudgetaryKbkDefaultsRequest MapToDomain(BudgetaryKbkDefaultsRequestDto dto)
        {
            return new BudgetaryKbkDefaultsRequest
            {
                KbkId = dto.KbkId,
                AccountCode = dto.AccountCode,
                Date = dto.Date,
                TradingObjectId = dto.TradingObjectId,
                Period = BudgetaryPeriodMapper.MapToDomain(dto.Period)
            };
        }

        internal static BudgetaryPaymentMetadataDto MapToDto(BudgetaryPaymentMetadata metadata)
        {
            return new BudgetaryPaymentMetadataDto
            {
                Accounts = MapPaymentReasonsToDto(metadata.Accounts),
                PaymentReasons = MapPaymentReasonsToDto(metadata.PaymentReasons),
                PaymentSubReasons = MapPaymentReasonsToDto(metadata.PaymentSubReasons),
                StatusOfPayers = MapStatusOfPayersToDto(metadata.StatusOfPayers),
            };
        }

        private static BudgetaryAccountDto[] MapPaymentReasonsToDto(IReadOnlyCollection<BudgetaryAccount> reasons)
        {
            return reasons.Select(x =>
                new BudgetaryAccountDto
                {
                    Code = x.Code,
                    Name = x.Name,
                    FullNumber = x.FullNumber,
                    DefaultPeriodType = x.DefaultPeriodType
                }).ToArray();
        }

        private static BudgetaryPaymentReasonDto[] MapPaymentReasonsToDto(IReadOnlyCollection<BudgetaryPaymentReason> reasons)
        {
            return reasons.Select(x =>
                new BudgetaryPaymentReasonDto
                {
                    Designation = x.Designation,
                    Description = x.Description,
                    Code = x.Code
                }).ToArray();
        }

        private static BudgetaryStatusOfPayerDto[] MapStatusOfPayersToDto(IReadOnlyCollection<BudgetaryStatusOfPayer> statuses)
        {
            return statuses.Select(x =>
                new BudgetaryStatusOfPayerDto
                {
                    Description = x.Description,
                    Code = x.Code
                }).ToArray();
        }

        internal static BudgetaryKbkDefaultsDto MapToDto(BudgetaryKbkDefaultsResponse response)
        {
            return new BudgetaryKbkDefaultsDto
            {
                PayerStatus = response.PayerStatus,
                PaymentBase = response.PaymentBase,
                PaymentType = response.PaymentType,
                DocumentDate = response.DocDate,
                DocumentNumber = response.DocNumber,
                Description = response.Description,
                Recipient = BudgetaryRecipientMapper.MapToDto(response.Recipient)
            };
        }

        internal static BudgetaryKbkAutocompleteResponseDto[] MapToDto(BudgetaryKbkResponse[] response)
        {
            if (response == null || response.Length == 0)
            {
                return [];
            }

            return response.Select(x => new BudgetaryKbkAutocompleteResponseDto
            {
                Id = x.Id,
                IsForIp = x.IsForIp,
                Name = x.Name,
                SubcontoId = x.SubcontoId
            }).ToArray();
        }

        internal static BudgetaryKbkRequest MapToDomain(BudgetaryKbkAutocompleteRequestDto dto)
        {
            return new BudgetaryKbkRequest
            {
                Query = dto.Query,
                AccountCode = dto.AccountCode,
                PaymentType = dto.PaymentType,
                Period = BudgetaryPeriodMapper.MapToDomain(dto.Period),
                Date = dto.Date
            };
        }

        private static string GetDocumentDate(BudgetaryPaymentSaveDto dto)
        {//из мастера оплаты усн приход¤т платежи по основанию «ƒ с валидной датой и не валидным номером
            if (dto.PaymentBase == BudgetaryPaymentBase.FreeDebtRepayment && dto.Date >= reasonChangeDate)
            {
                return string.IsNullOrWhiteSpace(dto.DocumentNumber) ? "0" : string.IsNullOrWhiteSpace(dto.DocumentDate)
                    ? "0"
                    : dto.DocumentDate;
            }
            return string.IsNullOrWhiteSpace(dto.DocumentDate)
                    ? "0"
                    : dto.DocumentDate;
        }

        internal static BudgetaryPaymentSaveRequest ToSaveRequest(this ConfirmBudgetaryPaymentDto dto)
        {
            return new BudgetaryPaymentSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,

                // Блок полей для ЕНП
                AccountCode = BudgetaryAccountCodes.UnifiedBudgetaryPayment, // ЕНП
                KbkPaymentType = KbkPaymentType.Payment,
                KbkId = null,
                KbkNumber = null,
                Period = new BudgetaryPeriod
                {
                    Date = dto.Date,
                    Type = BudgetaryPeriodType.Month,
                    Number = dto.Date.Month,
                    Year = dto.Date.Year,
                },
                PayerStatus = BudgetaryPayerStatus.Company,
                PaymentBase = BudgetaryPaymentBase.Other,
                PaymentType = BudgetaryPaymentType.Other,
                DocumentDate = "0",
                DocumentNumber = "0",
                Uin = null,
                Recipient = new BudgetaryRecipient(), // Важное поле. Устанавливается в BudgetaryPaymentOutsourceProcessor

                ProvideInAccounting = true,
                TaxPostings = new TaxPostingsData(),
                IsPaid = true,
                TradingObjectId = null,
                TaxationSystemType = null,
                PatentId = null,
                CurrencyInvoices = Array.Empty<DocumentLinkSaveRequest>(),

                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}