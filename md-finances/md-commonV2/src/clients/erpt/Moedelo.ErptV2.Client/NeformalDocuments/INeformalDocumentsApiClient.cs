using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.Demands;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;

namespace Moedelo.ErptV2.Client.NeformalDocuments
{
    public interface INeformalDocumentsApiClient : IDI
    {
        Task ParseNeformalDocumentWithNotificationAsync(int firmId, int userId, int neformalDocumentId);
    }
}