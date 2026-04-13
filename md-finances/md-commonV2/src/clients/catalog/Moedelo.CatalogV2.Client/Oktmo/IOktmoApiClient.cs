using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Oktmo
{
    public interface IOktmoApiClient : IDI
    {
        Task<string> GetByOkatoAsync(string okato);
    }
}