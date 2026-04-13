using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling.Dto;
using Moedelo.Billing.Abstractions.AutoBilling.ResultDto;

namespace Moedelo.Billing.Abstractions.AutoBilling;

public interface IAutoBillingInitiateApiClient
{
    Task<GetResultDto<InitiateDto>> GetAsync(GetInitiatesRequestDto requestDto);
    Task<InitiateDto> StartManualInitiateAsync(StartManualInitiateRequestDto requestDto);
    Task AddRequestsIntoInitiateAsync(AddRequestsIntoInitiateRequestDto requestDto);
    Task<InitiateDto> SetStateAsync(SetInitiateStateRequestDto requestDto);
    Task<InitiateDto> SetNextStateAsync(SetNextInitiateStateRequestDto requestDto);
    Task<InitiateDto> SetCancelStateAsync(SetCancelInitiateStateRequestDto requestDto);
}