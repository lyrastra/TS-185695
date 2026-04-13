using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HistoricalLogsV2.Client.EvotorLog
{
    public interface IEvotorLogClient : IDI
    {
        Task CreateSessionAsync(string updateUuid, int firmId, string sessionUuid);

        Task UpdateZReportDataAsync(string updateUuid, int firmId, string sessionUuid, string zReportData);

        Task UpdateRetailReportDataAsync(string updateUuid, int firmId, string sessionUuid, string retailReportData);

        Task UpdateRetailRefundDataAsync(string updateUuid, int firmId, string sessionUuid, string retailRefundData);

        Task SaveEvotorIntegrationLogAsync(string evotorUserId, DateTime requestDate, string type, string request, string response, string result, int? firmId = null);
    }
}