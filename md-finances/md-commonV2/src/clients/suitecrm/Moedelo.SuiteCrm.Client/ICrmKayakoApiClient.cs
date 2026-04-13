using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.SuiteCrm.Client
{
    public interface ICrmKayakoApiClient : IDI
    {
        Task<bool> GetFiles(DateTime dt);
    }
}