using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PaymentOrder;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.BudgetaryPaymentsClient
{
    public interface IBudgetaryPaymentsClient : IDI
    {
        Task<IList<BudgetaryPaymentDto>> GetUsnBudgetaryPrepaymentsAsync(int firmId, int userId, int year);
    }
}