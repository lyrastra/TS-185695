using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HistoricalLogsV2.Client
{
    public interface ITelemetryLogReaderClient : IDI
    {
        Task<List<TelemetryEventCountDto>> GetSummaryByPeriodAsync(DateTime startTime, DateTime endTime);
        Task<List<TelemetryLogEntryDto>> GetEventsByNameAndPeriodAsync(DateTime startTime, DateTime endTime, string eventName);
    }
}