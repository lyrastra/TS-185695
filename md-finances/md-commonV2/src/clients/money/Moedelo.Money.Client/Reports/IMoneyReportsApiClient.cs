using System.Threading.Tasks;
using Moedelo.Money.Dto.Reports;

namespace Moedelo.Money.Client.Reports
{
    public interface IMoneyReportsApiClient
    {
        Task QueryGetBankAndServiceReportAsync(DownloadGetBankAndServiceReportQueryDto dto);
    }
}
