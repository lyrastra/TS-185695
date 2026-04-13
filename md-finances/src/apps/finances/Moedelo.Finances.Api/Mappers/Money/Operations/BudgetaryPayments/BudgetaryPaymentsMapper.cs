using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.CatalogV2.Dto.Kbk;
using Moedelo.Common.Enums.Enums.KbkNumbers;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Operations.CashOrders;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.Finances.Dto.Money.Operations;
using Moedelo.Finances.Dto.Money.Operations.Requests;
using UnifiedBudgetarySubPaymentDto = Moedelo.Finances.Dto.Money.Operations.UnifiedBudgetarySubPaymentDto;

namespace Moedelo.Finances.Api.Mappers.Money.Operations.BudgetaryPayments
{
    public static class BudgetaryPaymentsMapper
    {
        public static T MapDtoToDomain<T>(BudgetaryAccPaymentsRequestDto dtoModel) where T : class, new()
        {
            switch (new T())
            {
                case BudgetaryPaymentOrderOperationQueryParams regularFilter:
                    return regularFilter.MapDtoToDomain(dtoModel) as T;
                case BudgetaryCashOrderOperationQueryParams cashFilter:
                    return cashFilter.MapDtoToDomain(dtoModel) as T;
            }

            throw new Exception("Unsupported domain type to map from dto");
        }

        private static BudgetaryCashOrderOperationQueryParams MapDtoToDomain(this BudgetaryCashOrderOperationQueryParams queryParams, BudgetaryAccPaymentsRequestDto dtoModel)
        {
            return new BudgetaryCashOrderOperationQueryParams
            {
                BudgetaryPeriodType = dtoModel.BudgetaryPeriodType,
                BudgetaryTaxesAndFees = dtoModel.BudgetaryTaxesAndFees,
                BudgetaryYear = dtoModel.BudgetaryYear,
                StartDate = dtoModel.StartDate,
                EndDate = dtoModel.EndDate,
                KbkId = dtoModel.KbkId,
                KbkPaymentType = dtoModel.KbkPaymentType,
                PaymentDirection = dtoModel.PaymentDirection,
                PatentId = dtoModel.PatentId
            };
        }

        private static BudgetaryPaymentOrderOperationQueryParams MapDtoToDomain(this BudgetaryPaymentOrderOperationQueryParams queryParams, BudgetaryAccPaymentsRequestDto dtoModel)
        {
            return new BudgetaryPaymentOrderOperationQueryParams
            {
                BudgetaryPeriodType = dtoModel.BudgetaryPeriodType,
                BudgetaryPeriodNumber = dtoModel.BudgetaryPeriodNumber,
                BudgetaryTaxesAndFees = dtoModel.BudgetaryTaxesAndFees ?? new List<int>(),
                BudgetaryYear = dtoModel.BudgetaryYear,
                StartDate = dtoModel.StartDate,
                EndDate = dtoModel.EndDate,
                KbkId = dtoModel.KbkId,
                KbkPaymentType = dtoModel.KbkPaymentType,
                PaymentDirection = dtoModel.PaymentDirection,
                PaidStatus = dtoModel.PaidStatus,
                PatentId = dtoModel.PatentId
            };
        }

        public static BudgetaryPaymentForReportDto MapDomainToDto(PaymentOrderOperation domainModel)
        {
            return new BudgetaryPaymentForReportDto
            {
                BudgetaryPeriodNumber = domainModel.BudgetaryPeriodNumber,
                KbkType = domainModel.KbkType,
                DocumentTypeDescription = domainModel.Description,
                IsPayment = domainModel.KbkPaymentType == KbkPaymentType.Payment || domainModel.KbkPaymentType == null,
                PaymentSnapshot = domainModel.PaymentSnapshot,
                BudgetaryTaxesAndFees = domainModel.BudgetaryTaxesAndFees,
                BudgetaryPeriodType = domainModel.BudgetaryPeriodType,
                Id = domainModel.Id,
                Sum = domainModel.Sum,
                KbkId = domainModel.KbkId,
                KbkNumber = domainModel.KbkNumber,
                OrderDate = domainModel.Date.ToString(),
                OrderNumber = domainModel.Number,
                PeriodDate = domainModel.BudgetaryPeriodDate.ToString(),
                BudgetaryPeriodYear = domainModel.BudgetaryPeriodYear,
                DocumentBaseId = domainModel.DocumentBaseId,
                OperationType = domainModel.OperationType,
                PatentId = domainModel.PatentId,
                TradingObjectId = domainModel.TradingObjectId,
                SubPayments = domainModel.SubPayments?.Select(MapDomainToDto).ToList() ?? new List<UnifiedBudgetarySubPaymentDto>()
            };
        }
        
        public static BudgetaryPaymentForReportDto MapDomainToDto(CashOrderOperation domainModel)
        {
            return new BudgetaryPaymentForReportDto
            {
                Id = domainModel.Id,
                DocumentBaseId = domainModel.DocumentBaseId,
                Sum = domainModel.Sum,
                OrderNumber = domainModel.Number,
                PeriodDate = domainModel.BudgetaryPeriodDate.ToString(),
                OrderDate = domainModel.Date.ToString(),
                KbkId = domainModel.KbkId,
                KbkNumber = domainModel.KbkNumber,
                KbkType = domainModel.KbkType,
                BudgetaryPeriodType = domainModel.BudgetaryPeriodType,
                BudgetaryPeriodNumber = domainModel.BudgetaryPeriodNumber,
                BudgetaryPeriodYear = domainModel.BudgetaryPeriodYear, 
                BudgetaryTaxesAndFees = domainModel.BudgetaryTaxesAndFees,
                IsPayment = domainModel.KbkPaymentType == KbkPaymentType.Payment || domainModel.KbkPaymentType == null,
                OperationType = domainModel.OperationType,
                PatentId = domainModel.PatentId,
                DocumentTypeDescription = domainModel.Destination,
                SubPayments = domainModel.SubPayments?.Select(MapDomainToDto).ToList() ?? new List<UnifiedBudgetarySubPaymentDto>()
            };
        }

        public static BudgetaryAccPaymentsRequestDto Map(GetBudgetaryAccPaymentsRequestDto dto)
        {
            return new BudgetaryAccPaymentsRequestDto
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PaymentDirection = dto.PaymentDirection,
                BudgetaryTaxesAndFees =
                    dto.BudgetaryTaxesAndFees == null
                        ? Array.Empty<int>()
                        : new [] { dto.BudgetaryTaxesAndFees.Value },
                KbkPaymentType = dto.KbkPaymentType,
                KbkId = dto.KbkId,
                PaidStatus = dto.PaidStatus,
                BudgetaryYear = dto.BudgetaryYear,
                BudgetaryPeriodType = dto.BudgetaryPeriodType,
                BudgetaryPeriodNumber = dto.BudgetaryPeriodNumber,
                NeedCashOperations = dto.NeedCashOperations,
                PatentId = dto.PatentId
            };
        }

        private static UnifiedBudgetarySubPaymentDto MapDomainToDto(UnifiedBudgetarySubPayment domainModel)
        {
            return new UnifiedBudgetarySubPaymentDto
            {
                DocumentBaseId = domainModel.DocumentBaseId,
                Sum = domainModel.Sum,
                Kbk = new KbkDto
                {
                    Id = domainModel.KbkId,
                    Number = domainModel.KbkNumber,
                    AccountCode = domainModel.AccountCode,
                    Type = domainModel.KbkType
                },
                PeriodType = domainModel.PeriodType,
                PeriodNumber = domainModel.PeriodNumber,
                PeriodYear = domainModel.PeriodYear,
                PatentId = domainModel.PatentId,
                TradingObjectId = domainModel.TradingObjectId
            };
        }
    }
}