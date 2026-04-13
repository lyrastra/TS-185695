using System.Threading.Tasks;

using Moedelo.BackofficeV2.Dto.Reports;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BackofficeV2.Client.Reports
{
    public interface IBackOfficeReportsClient : IDI
    {
        Task<bool> SendReportSale2DetailToEmailAsync(ReportSales2ParamDto param);

        Task<bool> SendReportLeadChannelsToEmailAsync(ReportLeadChannelsParamDto param);
    }
}
