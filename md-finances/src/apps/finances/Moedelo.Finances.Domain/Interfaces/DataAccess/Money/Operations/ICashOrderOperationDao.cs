using Moedelo.Finances.Domain.Models.Money.Operations.CashOrders;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations
{
    public interface ICashOrderOperationDao : IDI
    {
        Task<List<CashOrderOperation>> GetBudgetaryPaymentsAsync(int firmId, BudgetaryCashOrderOperationQueryParams queryParams);

        Task<List<CashOrderOperation>> GetBudgetaryPaymentsWithUnifiedTaxPaymentsAsync(int firmId, BudgetaryCashOrderOperationQueryParams queryParams);
    }
}