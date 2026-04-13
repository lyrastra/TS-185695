using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto;
using System;
using System.Threading.Tasks;

namespace Moedelo.RptV2.Client.AnalyticsApi
{
    public interface IAnalyticsApiClient : IDI
    {
        Task<FileDto> GetTurnoverFileAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
        Task<FileDto> GetPostingsFileAsync(int firmId, int userId, DateTime startDate, DateTime endDate);
    }
}
