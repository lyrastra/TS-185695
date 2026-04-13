using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.ProlongationTariffication;

namespace Moedelo.Billing.Abstractions.AutoBilling;

public interface IProlongationTarifficationApiClient
{
    Task<TarifficationContextDto> GetTarifficationContextAsync(TarifficationRequestDto dto);
    
    Task<TarifficationDto> GetTarifficationAsync(TarifficationRequestDto dto);
    
    Task<TarifficationDto> GetTarifficationResultAsync(TarifficationContextDto dto);
}