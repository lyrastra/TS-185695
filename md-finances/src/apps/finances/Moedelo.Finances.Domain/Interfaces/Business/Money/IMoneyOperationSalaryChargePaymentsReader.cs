using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money
{
    public interface IMoneyOperationSalaryChargePaymentsReader : IDI
    {
        Task<Dictionary<long, decimal>> GetSalaryChargePaymentSumByIdsAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds);
    }
}
