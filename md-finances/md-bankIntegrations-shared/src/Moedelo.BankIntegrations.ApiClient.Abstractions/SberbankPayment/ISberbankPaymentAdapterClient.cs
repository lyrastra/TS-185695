using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.SberbankAcceptance;
using Moedelo.BankIntegrations.ApiClient.Dto.SberbankPaymentRequest;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.SberbankPayment
{
    public interface ISberbankPaymentAdapterClient
    {
        Task<AdvanceAcceptanceReponseDto> GetAllowedAdvanceAcceptancesByFirmIdListAsync(IReadOnlyCollection<int> firmIds);

        Task<int> CreateAcceptanceAsync(SberbankAcceptanceDto dto);
    }
}