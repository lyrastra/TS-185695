using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.UnifiedBudgetaryPayments
{
    public interface IUnifiedBudgetaryPaymentsLaunchService
    {
        Task<DateTime> GetEnpStartDateAsync();
        Task<bool> IsEnpEnabledAsync(DateTime paymentDate);
    }
}