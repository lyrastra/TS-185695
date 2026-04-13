using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Bills.Dto;
using Moedelo.Billing.Abstractions.Dto.Bills;
using Moedelo.Billing.Abstractions.Dto.PurchasedServices;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Abstractions.Bills.Interfaces;

public interface IBillingBillsApiClient
{
    Task CreateNewBillingTrialAsync(TrialBillRequestDto requestDto);
    Task<PurchasedServicesDto> GetPurchasedServicesAsync(int firmId);
    Task<string> GetCurrentDurationAsync(int firmId, string productConfigurationCode);
    Task<InvoicedBillDto> InvoiceBillAsync(InvoiceBillRequestDto request, HttpQuerySetting setting = null);
    Task<RenewalCalculationResultDto> GetConfigurationCostAsync(GetConfigurationCostRequestDto request);
    Task<PackageParametersDto> GetPackageParametersAsync(GetPackageParametersRequestDto request);
}