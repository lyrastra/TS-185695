using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Balances;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.Balances
{
    public interface ISettlementSummaryApiClient: IDI
    {
        Task<List<SettlementBalanceDto>> GetAsync(int firmId, int userId, DateTime? onDate);
    }
}