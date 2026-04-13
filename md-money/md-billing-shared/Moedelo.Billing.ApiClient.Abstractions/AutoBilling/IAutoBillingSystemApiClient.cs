using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.System;

namespace Moedelo.Billing.Abstractions.AutoBilling;

public interface IAutoBillingSystemApiClient
{
    Task<AutoBillingSystemDto> GetAsync(AutoBillingSystemRequestDto requestDto);
    Task<AutoBillingSystemDto> SetAsync(SetAutoBillingSystemRequestDto requestDto);
}