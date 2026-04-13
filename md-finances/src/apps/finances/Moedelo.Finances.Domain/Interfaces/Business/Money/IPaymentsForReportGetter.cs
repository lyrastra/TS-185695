using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Operations.CashOrders;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IPaymentsForReportGetter : IDI
    {
        Task<List<PaymentOrderOperation>> GetBudgetaryPaymentsAsync(IUserContext userContext, BudgetaryPaymentOrderOperationQueryParams queryParams);
        Task<List<PaymentOrderOperation>> GetBudgetaryPaymentsWithSubPaymentsAsync(IUserContext userContext, BudgetaryPaymentOrderOperationQueryParams queryParams);
        Task<List<CashOrderOperation>> GetBudgetaryCashPaymentsAsync(IUserContext userContext, BudgetaryCashOrderOperationQueryParams queryParams);
        Task<List<CashOrderOperation>> GetBudgetaryCashPaymentsWithSubPaymentsAsync(IUserContext userContext, BudgetaryCashOrderOperationQueryParams queryParams);
    }
}