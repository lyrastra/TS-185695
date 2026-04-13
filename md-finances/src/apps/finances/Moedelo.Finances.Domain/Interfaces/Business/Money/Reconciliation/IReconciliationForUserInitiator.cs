using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation
{
    public interface IReconciliationForUserInitiator : IDI
    {
        Task<bool> InitiateAsync(IUserContext userContext, int settlementAccountId, DateTime onDate);
        Task<bool> InitiateAsync(IUserContext userContext, IReadOnlyCollection<MoneySourceBalance> balances, DateTime onDate);
    }
}
