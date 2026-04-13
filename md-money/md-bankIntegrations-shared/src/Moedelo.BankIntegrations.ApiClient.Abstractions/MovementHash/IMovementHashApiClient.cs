using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.ApiClient.Dto.MovementHashService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.MovementHash
{
    public interface IMovementHashApiClient
    {
        Task<bool> SaveMovementHashAsync(List<MovementHashDto> data);
        Task<List<bool>> CheckMovementHashList(List<MovementHashDto> data);
        Task<List<StatisticsOnIncompleteStatementDto>> GetStatisticsOnIncompleteStatementsAsync(DateTime startDate);
    }
}
