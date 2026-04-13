using Moedelo.Chat.Reports.Dto.SLA;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Threading.Tasks;

namespace Moedelo.Chat.Reports.Clients.SupportSLA
{
    public interface ISupportSLAClient: IDI
    {
        Task<SupportSLASummaryDto> GetTodaySupportSLAReportAsync();

        Task<SupportSLASummaryDto> GetSupportSLAReportAsync(DateTime start, DateTime end);
    }
}
