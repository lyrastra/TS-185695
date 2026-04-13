using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Rosstat.Client
{
    public interface IFormsApiClient : IDI
    {
        Task<bool> IsActivExistsAsync(int firmId, int userId);
    }
}