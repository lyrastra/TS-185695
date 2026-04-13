using System.Linq;
using System.Collections.Generic;
using Moedelo.Money.Enums;
using Moedelo.Money.Registry.DataAccess.OperationTypeSumByPeriod.DbModels;
using Moedelo.Money.Registry.Domain.Models.OperationTypeSumByPeriod;

namespace Moedelo.Money.Registry.DataAccess.OperationTypeSumByPeriod;

internal static class OperationTypeSumByPeriodMapper
{
    public static IReadOnlyList<OperationTypeSumByPeriodResult> ToDomain(IReadOnlyList<OperationTypeSumByPeriodDbModel> dbModels)
    {
        return dbModels
            .GroupBy(x => (x.Direction, x.Type, x.OperationDateYear, x.OperationDateMonth, x.PaidCardSum))
            .Select(group => new OperationTypeSumByPeriodResult
            {
                Direction = group.Key.Direction,
                Type = group.Key.Type,
                Sum = group.Sum(g => g.OperationSum),
                Period = new MonthPeriod(group.Key.OperationDateYear, group.Key.OperationDateMonth),
                PaidCardSum = group.Key.PaidCardSum,
                AcquiringCommissions = group.Key.Type == OperationType.MemorialWarrantRetailRevenue
                    ? group.Where(c => c.AcquiringCommissionSum.HasValue).Select(c => new AcquiringCommissionSumByPeriod
                    {
                        Period = new MonthPeriod(c.AcquiringCommissionDateYear.Value, c.AcquiringCommissionDateMonth.Value),
                        Sum = c.AcquiringCommissionSum.Value
                    }).ToArray()
                    : null
            })
            .ToList();
    }
}
