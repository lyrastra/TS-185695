using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Commands;
using Subconto = Moedelo.Money.Domain.AccPostings.Subconto;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class DeductionMapper
    {
        public static DeductionImportRequest Map(ImportDeduction commandData)
        {
            return new DeductionImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Imported,
                Kbk = commandData.Kbk,
                Oktmo = commandData.Oktmo,
                Uin = commandData.Uin,
                PayerStatus = commandData.PayerStatus,
                DeductionWorkerId = commandData.DeductionWorkerId,
                DeductionWorkerInn = commandData.DeductionWorkerInn,
                IsBudgetaryDebt = commandData.IsBudgetaryDebt,
                DeductionWorkerDocumentNumber = commandData.DeductionWorkerDocumentNumber,
                PaymentPriority = commandData.PaymentPriority,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static DeductionImportRequest Map(ImportDeductionDuplicate commandData)
        {
            return new DeductionImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
                DuplicateId = commandData.DuplicateId,
                OperationState = OperationState.Duplicate,
                Kbk = commandData.Kbk,
                Oktmo = commandData.Oktmo,
                Uin = commandData.Uin,
                PayerStatus = commandData.PayerStatus,
                DeductionWorkerId = commandData.DeductionWorkerId,
                DeductionWorkerInn = commandData.DeductionWorkerInn,
                IsBudgetaryDebt = commandData.IsBudgetaryDebt,
                DeductionWorkerDocumentNumber = commandData.DeductionWorkerDocumentNumber,
                PaymentPriority = commandData.PaymentPriority,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static DeductionImportRequest Map(ImportDeductionWithMissingContractor commandData)
        {
            return new DeductionImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.MissingKontragent,
                Kbk = commandData.Kbk,
                Oktmo = commandData.Oktmo,
                Uin = commandData.Uin,
                PayerStatus = commandData.PayerStatus,
                IsBudgetaryDebt = commandData.IsBudgetaryDebt,
                PaymentPriority = commandData.PaymentPriority,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static DeductionImportRequest Map(ImportDeductionWithMissingEmployee commandData)
        {
            return new DeductionImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.MissingWorker,
                Kbk = commandData.Kbk,
                Oktmo = commandData.Oktmo,
                Uin = commandData.Uin,
                PayerStatus = commandData.PayerStatus,
                IsBudgetaryDebt = commandData.IsBudgetaryDebt,
                PaymentPriority = commandData.PaymentPriority,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static DeductionResponseDto Map(DeductionResponse response)
        {
            return new DeductionResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Contractor),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport,
                ProvideInAccounting = response.ProvideInAccounting,
                Kbk = response.Kbk == "0" ? string.Empty : response.Kbk,
                Oktmo = response.Oktmo,
                Uin = string.IsNullOrWhiteSpace(response.Uin) ? "0" : response.Uin,
                DeductionWorkerDocumentNumber = response.DeductionWorkerDocumentNumber,
                PaymentPriority = response.PaymentPriority,
                PayerStatus = response.PayerStatus,
                DeductionWorkerId = response.DeductionWorkerId,
                DeductionWorkerInn = response.DeductionWorkerInn == "0" ? string.Empty : response.DeductionWorkerInn,
                DeductionWorkerFio = response.DeductionWorkerFio
            };
        }

        public static DeductionSaveRequest Map(DeductionSaveDto dto)
        {
            var result = new DeductionSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract?.DocumentBaseId,
                Description = dto.Description,
                IsPaid = dto.IsPaid,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                IsBudgetaryDebt = dto.IsBudgetaryDebt,
                PaymentPriority = dto.PaymentPriority,
                AccountingPosting = MapCustomAccPosting(dto),
                PayerStatus = dto.PayerStatus,
                IsSaveNumeration = dto.IsSaveNumeration,
                Uin = dto.Uin
            };

            if (dto.IsBudgetaryDebt)
            {
                result.Oktmo = dto.Oktmo;
                result.Kbk = dto.Kbk;
                result.DeductionWorkerDocumentNumber = dto.DeductionWorkerDocumentNumber;
                result.DeductionWorkerId = dto.DeductionWorkerId;
                result.DeductionWorkerInn = dto.DeductionWorkerInn;
            }

            return result;
        }

        public static DeductionSaveRequest ToSaveRequest(this ConfirmDeductionDto dto)
        {
            var result = new DeductionSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = null,
                Description = dto.Description,
                IsPaid = true,
                ProvideInAccounting = true,
                OperationState = dto.Contractor?.Id > 0
                    ? OperationState.OutsourceApproved
                    : OperationState.MissingKontragent,
                OutsourceState = null,
                // БУ заполнится в бизнес-слое 
                AccountingPosting = null,
                // В ЛК на 23.11.2023 доступно 1 и 4 (подставляем по умолчанию: 1)
                PaymentPriority = PaymentPriority.First,
                PayerStatus = BudgetaryPayerStatus.None,
                // "Вызыскивается долг перед бюджетом": нет + не заполняем связанные с этим поля 
                IsBudgetaryDebt = false,
                Oktmo = null,
                Kbk = null,
                Uin = "0",
                DeductionWorkerDocumentNumber = null,
                DeductionWorkerId = null,
                DeductionWorkerInn = null,
            };

            return result;
        }
        
        public static PaymentDetailDto MapToPaymentDetail(DeductionResponse response)
        {
            return new PaymentDetailDto
            {
                Inn = response.Contractor.Inn,
                PayeeSettlementAccount = response.Contractor.SettlementAccount,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        private static DeductionCustomAccPosting MapCustomAccPosting(DeductionSaveDto dto)
        {
            if (dto.ProvideInAccounting != true || !dto.IsPaid || dto.AccountingPosting == null)
            {
                return null;
            }

            var postingDto = dto.AccountingPosting;
            return new DeductionCustomAccPosting
            {
                Date = dto.Date.Date,
                Sum = postingDto.Sum,
                DebitSubconto = postingDto.DebitSubconto == null
                    ? Array.Empty<Subconto>()
                    : Map(postingDto.DebitSubconto),
                DebitCode = (int)postingDto.DebitCode,
                CreditSubconto = postingDto.CreditSubconto,
                Description = postingDto.Description
            };
        }

        private static Subconto[] Map(IReadOnlyCollection<SubcontoDto> debitSubconto)
        {
            return debitSubconto.Select(x => new Subconto { Id = x.Id, Name = x.Name }).ToArray();
        }
    }
}