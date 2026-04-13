using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling.Dto;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.Report;

namespace Moedelo.Billing.Abstractions.AutoBilling;

public interface IAutoBillingReportApiClient
{
    Task<AutoBillingReportResponseDto> GetAsync(GetAutoBillingReportRequestDto requestDto);
}