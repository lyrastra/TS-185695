using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.DataAccess.CloseCashGroup
{
    public interface ICloseCashGroupDao : IDI
    {
        Task<DateTime?> GetLastClosedCashDateAsync(int firmId);
    }
}
