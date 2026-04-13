using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.DownloadReport;
using Moedelo.RptV2.Dto.File;

namespace Moedelo.RptV2.Client.DownloadReport
{
    public interface IDownloadReportApiClient : IDI
    {
        Task<FileResultDto> DownloadPaymentOrder(DownloadPaymentOrderRequestDto dto);

        Task<FileResultDto> DownloadBill(DownloadBillRequestDto dto);
        
        Task<FileResultDto[]> DownloadBills(DownloadBillsRequestDto dto);
        
        Task<FileResultDto> DownloadSzvm(int firmId, int userId, DownloadSzvmRequestDto dto);
        
        Task<FileResultDto> DownloadSzvExperience(int firmId, int userId, DownloadSzvExperienceRequestDto dto);
    }
}
