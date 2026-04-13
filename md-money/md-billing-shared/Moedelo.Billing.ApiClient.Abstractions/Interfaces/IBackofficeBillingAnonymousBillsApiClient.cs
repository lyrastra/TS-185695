using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto;
using Moedelo.Billing.Abstractions.Dto.BackofficeBillingAnonymousBills;

namespace Moedelo.Billing.Abstractions.Interfaces;

public interface IBackofficeBillingAnonymousBillsApiClient
{
    Task<CostsResponseDto> CalculateCostAsync(AnonymousCostRequestDto request);
}