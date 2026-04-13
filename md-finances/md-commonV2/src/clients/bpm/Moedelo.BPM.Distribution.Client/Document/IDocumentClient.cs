using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.BPM.Distribution.Client.Document
{
    public interface IDocumentClient : IDI
    {
        Task<HttpFileModel> GetDocument(string documentId);
    }
}