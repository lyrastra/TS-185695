using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.ErptV2.Dto.AutoComplete;

namespace Moedelo.ErptV2.Client.AutoComplete
{
    public interface IErptAutocompleteClient : IDI
    {
        Task<GetNdsXmlFilesResponse> GetNdsXmlFilesAutocomplete(int firmId, int userId, GetNdsXmlFilesRequest request);
    }
}