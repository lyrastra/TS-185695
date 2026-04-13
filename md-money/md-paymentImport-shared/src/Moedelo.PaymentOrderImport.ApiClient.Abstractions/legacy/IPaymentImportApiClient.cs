using Moedelo.Common.Types;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.legacy
{
    public interface IPaymentImportApiClient
    {
        Task ImportFromIntegrationAsync(FirmId firmId, UserId userId, string fileId, bool isManual = false, HttpQuerySetting setting = null);

        Task ImportFromUserAsync(FirmId firmId, UserId userId, ImportFromUserDto dto, HttpQuerySetting setting = null);

        Task AddImportMessageAsync(FirmId firmId, string message, HttpQuerySetting setting = null);
    }
}
