using System.Threading.Tasks;
using Moedelo.BPM.Registry.Documents.Client.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BPM.Registry.Documents.Client
{
    public interface IDocumentsRegistryApiClient : IDI
    {
        Task<RegistryDto[]> GetAsync(int firmId, CategoryTypeDto? category = null, int? operationType = null, bool? completed = null);
    }
}