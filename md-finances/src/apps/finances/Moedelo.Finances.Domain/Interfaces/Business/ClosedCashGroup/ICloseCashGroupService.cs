using System;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.ClosedCashGroup
{
    public interface ICloseCashGroupService :IDI
    {
        Task<DateTime?> GetLastClosedCashDateAsync(int firmId);
    }
}
