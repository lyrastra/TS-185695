using Moedelo.Finances.Domain.Models;
using Moedelo.Finances.Public.ClientData;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Finances.Public.Mappers
{
    public static class ReportsMapper
    {
        public static List<Period> MapToDomain(this IReadOnlyCollection<PeriodClientData> periodClientData)
        {
            return periodClientData.Select(p =>
            {
                return new Period
                {
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                };
            }).ToList();
        }
    }

}