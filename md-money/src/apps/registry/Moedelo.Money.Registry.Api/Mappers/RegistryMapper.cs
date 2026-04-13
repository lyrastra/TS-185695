using Moedelo.Money.Registry.Dto;
using Moedelo.Money.Registry.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Registry.Domain.Models.SelfCostPayments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Registry.Api.Mappers
{
    static class RegistryMapper
    {
        public static RegistryQuery MapToDomain(RegistryQueryDto request)
        {
            return new RegistryQuery
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Limit = request.Limit,
                Offset = request.Offset,
                AfterDate = request.AfterDate,
                TaxationSystemType = request.TaxationSystemType,
                OperationSource = request.OperationSource,
                OperationTypes = request.OperationTypes,
                ContractorId = request.ContractorId,
                ContractorType = request.ContractorType,
                Query = request.Query,
                DocumentBaseIds = request.DocumentBaseIds
            };
        }

        public static RegistryOperationDto[] MapToResponse(IReadOnlyCollection<MoneyOperation> operations)
        {
            return operations.Select(x => new RegistryOperationDto
            {
                DocumentBaseId = x.DocumentBaseId,
                Number = x.Number,
                Date = x.Date,
                Sum = x.Sum,
                Contractor = MapContractor(x.Contractor),
                Direction = x.Direction,
                Source = MapSource(x.Source),
                KontragentSettlementAccountId = x.KontragentSettlementAccountId,
                OperationType = x.OperationType,
                Description = x.Description,
                ModifyDate = x.ModifyDate,
                IsPaid = x.IsPaid,
                PatentId = x.PatentId,
                TaxationSystemType = x.TaxationSystemType,
                IncludeNds = x.IncludeNds,
                NdsSum = x.NdsSum,
                NdsType = x.NdsType,
                IsMainContractor = x.Direction == MoneyDirection.Incoming
                    ? x.KontragentAccountCode == 620200
                    : x.KontragentAccountCode == 600200,
            }).ToArray();
        }

        private static RegistryOperationContractorDto MapContractor(Contractor contractor)
        {
            if (contractor == null)
            {
                return null;
            }
            return new RegistryOperationContractorDto
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Type = contractor.Type
            };
        }

        private static RegistryOperationSourceDto MapSource(Source source)
        {
            return new RegistryOperationSourceDto
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type
            };
        }

        public static SelfCostPaymentRequest MapToDomain(SelfCostPaymentRequestDto request)
        {
            return new SelfCostPaymentRequest
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Limit = request.Limit,
                Offset = request.Offset,
                Source = request.Source
            };
        }

        public static SelfCostPaymentResponseDto[] MapToResponse(IReadOnlyCollection<SelfCostPayment> payments)
        {
            return payments.Select(x => new SelfCostPaymentResponseDto
            {
                DocumentBaseId = x.DocumentBaseId,
                Number = x.Number,
                Date = x.Date,
                Sum = x.Sum,
                RubSum = x.Type == OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier
                    ? x.RubSum
                    : null,
                Type = x.Type
            }).ToArray();
        }
    }
}
