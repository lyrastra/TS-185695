using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using Moedelo.ErptV2.Dto.FnsNeformalDocuments;

namespace Moedelo.ErptV2.Client.NeformalDocuments
{
    public interface IFnsNeformalDocumentsClient : IDI
    {
        Task<List<FnsNeformalDocumentsCountForFirmDto>> GetCountsForFirmsAsync(List<int> firmIds);
    }
}