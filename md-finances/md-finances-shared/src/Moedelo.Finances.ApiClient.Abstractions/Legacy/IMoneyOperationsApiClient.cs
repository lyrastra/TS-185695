using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Finances.ApiClient.Abstractions.Legacy
{
    public interface IMoneyOperationsApiClient
    {
        Task<IEnumerable<BudgetaryPaymentForReportDto>> GetBudgetaryPaymentsV2Async(FirmId firmId, UserId userId,
            BudgetaryAccPaymentsRequestDto request);
    }
}