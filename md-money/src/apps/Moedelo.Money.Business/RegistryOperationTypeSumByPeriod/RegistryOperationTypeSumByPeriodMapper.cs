using System.Linq;
using System.Collections.Generic;
using Moedelo.Money.Domain.Registry.OperationTypeSumByPeriod;
using Moedelo.Money.Registry.Dto.OperationTypeSumByPeriod;

namespace Moedelo.Money.Business.RegistryOperationTypeSumByPeriod;

static class RegistryOperationTypeSumByPeriodMapper
{
    public static OperationTypeSumByPeriodRequestDto MapToRequest(OperationTypeSumByPeriodRequest model)
    {
        return new OperationTypeSumByPeriodRequestDto
        {
            OperationTypes = model.OperationTypes,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
        };
    }

    public static OperationTypeSumByPeriodResponse MapToResponse(OperationTypeSumByPeriodResponseDto dto)
    {
        return new OperationTypeSumByPeriodResponse
        {
            Type = dto.Type,
            Direction = dto.Direction,
            Period = new MonthPeriod(dto.Period.Year, dto.Period.Month),
            Sum = dto.Sum,
            PaidCardSum = dto.PaidCardSum,
            AcquiringCommissions = MapToResponse(dto.AcquiringCommissions),
        };
    }

    private static AcquiringCommissionSumByPeriod[] MapToResponse(IReadOnlyCollection<AcquiringCommissionSumByPeriodDto> dtos)
    {
        return dtos?.Select(x => new AcquiringCommissionSumByPeriod
            {
                Period = new MonthPeriod(x.Period.Year, x.Period.Month),
                Sum = x.Sum,
            }).ToArray();
    }
}
