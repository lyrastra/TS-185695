using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IDocumentRequisitesApiClient
    {
        Task<DocumentRequisitesDto> GetAsync(FirmId firmId, UserId userId);
    }
}