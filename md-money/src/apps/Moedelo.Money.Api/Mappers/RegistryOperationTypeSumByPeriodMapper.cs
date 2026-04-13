using System.Linq;
using System.Collections.Generic;
using Moedelo.Money.Api.Models.Registry.OperationTypeSumByPeriod;
using Moedelo.Money.Domain.Registry.OperationTypeSumByPeriod;

namespace Moedelo.Money.Api.Mappers;

static class RegistryOperationTypeSumByPeriodMapper
{
    public static OperationTypeSumByPeriodRequest MapToDomain(OperationTypeSumByPeriodRequestDto request)
    {
        return new OperationTypeSumByPeriodRequest
        {
            OperationTypes = request.OperationTypes?.ToArray(),
            EndDate = request.EndDate,
            StartDate = request.StartDate,
        };
    }

    public static OperationTypeSumByPeriodResponseDto[] MapToDto(OperationTypeSumByPeriodResponse[] models)
    {
        return models.Select(x => new OperationTypeSumByPeriodResponseDto
        {
            Type = x.Type,
            Direction = x.Direction,
            Period = new MonthPeriodDto(x.Period.Year, x.Period.Month),
            Sum = x.Sum,
            PaidCardSum = x.PaidCardSum,
            AcquiringCommissions = MapToDto(x.AcquiringCommissions),
        }).ToArray();
    }

    private static AcquiringCommissionSumByPeriodDto[] MapToDto(IReadOnlyCollection<AcquiringCommissionSumByPeriod> models)
    {
        return models?.Select(x => new AcquiringCommissionSumByPeriodDto
        {
            Period = new MonthPeriodDto(x.Period.Year, x.Period.Month),
            Sum = x.Sum,
        }).ToArray();
    }
}
