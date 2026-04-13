using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.BackofficeBilling.Bills;
using Moedelo.BillingV2.Dto.BackofficeBilling.Bills.BillRequest;
using Moedelo.BillingV2.Dto.BackofficeBilling.Cost;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BillingV2.Client.BackofficeBillingBills
{
    public interface IBackofficeBillingBillsApiClient : IDI
    {
        Task<BackofficeBillingBillResponseDto> GetBackofficeBillByPrimaryBillIdAsync(int primaryBillId);

        Task<int> InvoiceBillAsync(BillRequestDto request);

        Task<CostsResponseDto> CalculateCostAsync(BillRequestDto request);

        Task<BackofficeBillingBillResponseDto> InvoiceBillAndGetInfoAsync(BillRequestDto request);

        Task<int> InvoiceAndSwitchOnAsync(BillRequestDto request);
    }
}