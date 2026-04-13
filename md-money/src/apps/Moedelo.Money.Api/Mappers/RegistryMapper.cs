using Moedelo.Money.Api.Models.Registry;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;
using Moedelo.Money.Domain.Registry;
using Moedelo.Money.Enums;
using ContractorDto = Moedelo.Money.Api.Models.Registry.ContractorDto;
using NdsDto = Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.NdsDto;
using RegistryQueryDto = Moedelo.Money.Api.Models.Registry.RegistryQueryDto;

namespace Moedelo.Money.Api.Mappers
{
    static class RegistryMapper
    {
        public static RegistryQuery MapToDomain(RegistryQueryDto request)
        {
            return new RegistryQuery
            {
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                Limit = request.Limit,
                Offset = request.Offset,
                AfterDate = request.AfterDate,
                TaxationSystemType = request.TaxationSystemType,
                OperationSource = request.OperationSource,
                OperationTypes = request.OperationTypes,
                ContractorId = request.ContractorId,
                ContractorType = request.ContractorType != null ? (ContractorType)request.ContractorType.Value : ContractorType.Kontragent,
                Query = request.Query
            };
        }

        public static RegistryQuery MapToDomain(PrivateRegistryQueryDto request)
        {
            return new RegistryQuery
            {
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                Limit = request.Limit,
                Offset = request.Offset,
                AfterDate = request.AfterDate,
                TaxationSystemType = request.TaxationSystemType,
                OperationSource = request.OperationSource,
                OperationTypes = request.OperationTypes,
                ContractorId = request.ContractorId,
                ContractorType = request.ContractorType != null ? (ContractorType)request.ContractorType.Value : ContractorType.Kontragent,
                Query = request.Query,
                DocumentBaseIds = request.DocumentBaseIds
            };
        }

        public static OperationResponseDto MapOperationToDto(RegistryOperation dto)
        {
            return new OperationResponseDto
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Contractor = MapContractorToDto(dto.Contractor),
                Direction = dto.Direction,
                OperationType = dto.OperationType,
                Source = MapSourceToDto(dto.Source),
                KontragentSettlementAccountId = dto.KontragentSettlementAccountId,
                Description = dto.Description,
                Sum = dto.Sum,
                ModifyDate = dto.ModifyDate,
                IsPaid = dto.IsPaid,
                PatentId = dto.PatentId,
                TaxationSystemType = dto.TaxationSystemType,
                Nds = MapNds(dto),
                IsMainContractor = dto.IsMainContractor
            };
        }

        private static NdsDto MapNds(RegistryOperation operation)
        {
            return new NdsDto
            {
                IncludeNds = operation.IncludeNds,
                Type = operation.IncludeNds
                    ? operation.NdsType
                    : null,
                Sum = operation.IncludeNds
                    ? operation.NdsSum
                    : null
            };
        }

        private static ContractorDto MapContractorToDto(Contractor contractor)
        {
            if (contractor == null)
            {
                return null;
            }
            return new ContractorDto
            {
                Id = contractor.Id,
                Type = contractor.Type,
                Name = contractor.Name
            };
        }

        private static SourceDto MapSourceToDto(Source source)
        {
            return new SourceDto
            {
                Id = source.Id,
                Type = source.Type,
                Name = source.Name
            };
        }
    }
}
