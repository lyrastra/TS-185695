using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.OAuthClientId.Dto;

namespace Moedelo.OAuthClientId.Client
{
    public interface IOAuthClientIdApiClient
    {
        Task<List<OAuthClientIdDto>> GetAllAsync();
    }
}
