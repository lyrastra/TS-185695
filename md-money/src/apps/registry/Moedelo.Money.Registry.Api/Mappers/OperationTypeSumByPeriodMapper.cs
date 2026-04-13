using System.Linq;
using System.Collections.Generic;
using Moedelo.Money.Registry.Dto.OperationTypeSumByPeriod;
using Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

namespace Moedelo.Money.Registry.Api.Mappers;

static class OperationTypeSumByPeriodMapper
{
    public static OperationTypeSumByPeriodRequest MapToDomain(OperationTypeSumByPeriodRequestDto request)
    {
        return new OperationTypeSumByPeriodRequest
        {
            OperationTypes = request.OperationTypes?.ToArray(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };
    }

    public static OperationTypeSumByPeriodResponseDto[] MapToResponse(IReadOnlyCollection<OperationTypeSumByPeriodResult> models)
    {
        return models.Select(x => new OperationTypeSumByPeriodResponseDto
        {
            Type = x.Type,
            Direction = x.Direction,
            Period = new MonthPeriodDto(x.Period.Year, x.Period.Month),
            Sum = x.Sum,
            PaidCardSum = x.PaidCardSum,
            AcquiringCommissions = MapToResponse(x.AcquiringCommissions)
        }).ToArray();
    }

    private static AcquiringCommissionSumByPeriodDto[] MapToResponse(IReadOnlyCollection<AcquiringCommissionSumByPeriod> models)
    {
        return models?.Select(x => new AcquiringCommissionSumByPeriodDto
        {
            Period = new MonthPeriodDto(x.Period.Year, x.Period.Month),
            Sum = x.Sum,
        }).ToArray();
    }
}
