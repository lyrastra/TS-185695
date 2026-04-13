using System;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.Utils.NdsPercentage
{
    public interface INdsPercentageService : IDI
    {
        decimal GetCurrentRate();

        decimal GetRateForDate(DateTime date);

        Dictionary<DateTime, decimal> GetRateForDates(List<DateTime> dates);
    }
}