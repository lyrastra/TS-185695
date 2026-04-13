using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Dto.Money;
using Moedelo.Finances.Dto.Money.Duplicates;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Api.Mappers.Money
{
    public interface IDuplicatesMapper : IDI
    {
        DuplicateOperationRequest Map(DuplicateIncomingOperationRequestDto dto);
        DuplicateOperationRequest Map(DuplicateOutgoingOperationRequestDto dto);
        DuplicateOperationRequest Map(DuplicateBankFeeOutgoingOperationRequestDto dto);
        DuplicateOperationRequest Map(DuplicateRoboAndSapeOperationRequestDto dto);
        DuplicateOperationRequest Map(DuplicateYandexOperationRequestDto dto);
        DuplicateOperationRequest Map(DuplicateMovementYandexOperationRequestDto dto);
        DuplicateResultDto Map(DuplicateResult result);
    }
}