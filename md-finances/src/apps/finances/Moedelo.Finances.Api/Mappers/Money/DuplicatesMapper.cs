using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Dto.Money.Duplicates;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataMapping;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.Finances.Api.Mappers.Money
{
    [InjectAsSingleton]
    public class DuplicatesMapper : IDuplicatesMapper
    {
        private readonly IAutoMapper autoMapper;

        public DuplicatesMapper(
            IAutoMapper autoMapper)
        {
            this.autoMapper = autoMapper;
        }

        public DuplicateOperationRequest Map(DuplicateIncomingOperationRequestDto dto)
        {
            var result = autoMapper.Map<DuplicateIncomingOperationRequestDto, DuplicateOperationRequest>(dto);
            result.SettlementAccountId = dto.SettlementAccountId;
            return result;
        }

        public DuplicateOperationRequest Map(DuplicateOutgoingOperationRequestDto dto)
        {
            var result = autoMapper.Map<DuplicateOutgoingOperationRequestDto, DuplicateOperationRequest>(dto);
            result.SettlementAccountId = dto.SettlementAccountId;
            return result;
        }

        public DuplicateOperationRequest Map(DuplicateBankFeeOutgoingOperationRequestDto dto)
        {
            var result = autoMapper.Map<DuplicateBankFeeOutgoingOperationRequestDto, DuplicateOperationRequest>(dto);
            result.SettlementAccountId = dto.SettlementAccountId;
            result.DestinationDescription = dto.Description;
            return result;
        }

        public DuplicateOperationRequest Map(DuplicateRoboAndSapeOperationRequestDto dto)
        {
            var result = autoMapper.Map<DuplicateRoboAndSapeOperationRequestDto, DuplicateOperationRequest>(dto);
            result.DestinationDescription = dto.Description;
            return result;
        }

        public DuplicateOperationRequest Map(DuplicateYandexOperationRequestDto dto)
        {
            var result = autoMapper.Map<DuplicateYandexOperationRequestDto, DuplicateOperationRequest>(dto);
            result.KontragentId = dto.KontragentId;
            result.DestinationDescription = dto.Description;
            return result;
        }

        public DuplicateOperationRequest Map(DuplicateMovementYandexOperationRequestDto dto)
        {
            return autoMapper.Map<DuplicateMovementYandexOperationRequestDto, DuplicateOperationRequest>(dto);
        }

        public DuplicateResultDto Map(DuplicateResult result)
        {
            return new DuplicateResultDto
            {
                Id = result.Id,
                BaseId = result.BaseId,
                IsStrict = result.IsStrict
            };
        }

        public static DuplicateDetectionRequest Map(int firmId, bool isAccounting, DuplicateDetectionRequestDto dto)
        {
            return new DuplicateDetectionRequest
            {
                FirmId = firmId,
                IsAccounting = isAccounting,
                SettlementAccountId = dto.SettlementAccountId,
                Operations = dto.Operations.Select(Map).ToArray()
            };
        }

        private static OperationForDuplicateDetection Map(OperationForDuplicateDetectionDto dto)
        {
            return new OperationForDuplicateDetection
            {
                Guid = dto.Guid,
                Direction = dto.Direction,
                Date = dto.Date,
                Sum = dto.Sum,
                Number = dto.Number,
                ContractorInn = dto.ContractorInn,
                ContractorSettlementAccount = dto.ContractorSettlementAccount,
                Description = dto.Description,
                OperationType = (OperationType)dto.OperationType
            };
        }

        public static DuplicateDetectionResultDto[] Map(IReadOnlyCollection<DuplicateDetectionResult> results)
        {
            return results.Select(Map).ToArray();
        }

        private static DuplicateDetectionResultDto Map(DuplicateDetectionResult x)
        {
            return new DuplicateDetectionResultDto
            {
                Guid = x.Guid,
                SourceId = x.SourceId,
                SourceBaseId = x.SourceBaseId,
                IsStrict = x.IsStrict
            };
        }
    }
}