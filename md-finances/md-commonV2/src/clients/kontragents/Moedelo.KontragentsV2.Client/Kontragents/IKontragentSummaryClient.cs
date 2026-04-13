using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KontragentsV2.Dto.KontragentSummary;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    public interface IKontragentSummaryClient : IDI
    {
        /// <summary>
        /// Поставщики, по которым сумма списаний по р/счету и кассе была наибольшая
        /// </summary>
        Task<List<FirmKontragentSumSummaryDto>> GetTopSellersAsync(IReadOnlyCollection<int> firmIds, int count, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Покупатели, по которым сумма поступлений по р/счету и кассе была наибольшая
        /// </summary>
        Task<List<FirmKontragentSumSummaryDto>> GetTopCustomersAsync(IReadOnlyCollection<int> firmIds, int count, DateTime startDate, DateTime endDate);
    }
}