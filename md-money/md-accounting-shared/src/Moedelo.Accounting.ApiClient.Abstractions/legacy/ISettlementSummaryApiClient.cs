using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface ISettlementSummaryApiClient
    {
        Task<List<SettlementBalanceDto>> GetAsync(int firmId, int userId, DateTime? onDate);
    }
}