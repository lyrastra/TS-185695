using System;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.NdflPayments.Dto;

namespace Moedelo.Payroll.ApiClient.Abstractions.NdflPayments
{
    public interface INdflPaymentsApiClient
    {
        Task<NdflPaymentsDataDto> GetDataAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate);
    }
}