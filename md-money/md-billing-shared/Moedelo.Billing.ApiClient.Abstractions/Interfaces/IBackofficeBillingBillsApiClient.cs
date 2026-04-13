using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Abstractions.Interfaces;

public interface IBackofficeBillingBillsApiClient
{
    Task<BackofficeBillingBillResponseDto> GetBackofficeBillByPrimaryBillIdAsync(int primaryBillId);

    Task<int> InvoiceBillAsync(BillRequestDto request);

    Task<CostsResponseDto> CalculateCostAsync(BillRequestDto request);

    Task<BackofficeBillingBillResponseDto> InvoiceBillAndGetInfoAsync(BillRequestDto request, HttpQuerySetting setting = null);

    Task<int> InvoiceAndSwitchOnAsync(BillRequestDto request);
}