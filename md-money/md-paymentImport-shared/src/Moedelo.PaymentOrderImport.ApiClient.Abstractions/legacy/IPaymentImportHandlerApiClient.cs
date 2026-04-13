using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.legacy 
{

    public interface IPaymentImportHandlerApiClient
    {
        Task<FilePreImportStatusDto> GetImportFileStatus(int firmId, string fileId);

        Task<StartBPMImportResultDto> StartBpmImport(StartBPMImportDto dto, HttpQuerySetting setting = null);


    }
}