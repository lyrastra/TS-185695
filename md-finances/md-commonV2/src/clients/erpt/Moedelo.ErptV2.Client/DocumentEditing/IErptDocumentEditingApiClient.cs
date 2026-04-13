using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.DocumentEditing;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.ErptV2.Client.DocumentEditing
{
    public interface IErptDocumentEditingApiClient : IDI
    {
        Task AddAsync(SaveEditingDataDto data);
    }
}