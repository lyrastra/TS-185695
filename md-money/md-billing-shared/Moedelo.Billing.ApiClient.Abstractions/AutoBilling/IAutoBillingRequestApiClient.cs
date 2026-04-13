using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling.Dto;
using Moedelo.Billing.Abstractions.AutoBilling.ResultDto;

namespace Moedelo.Billing.Abstractions.AutoBilling;

public interface IAutoBillingRequestApiClient
{
    Task<GetResultDto<RequestDto>> GetAsync(GetRequestsRequestDto requestsRequestDto);
    Task<RequestDto> SetStateAsync(SetRequestStateRequestDto requestDto);
}