using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Docs;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Docs.Client.Docs
{
    public interface IDocsApiClient : IDI
    {
        Task<List<NotProvideDocumentResultDto>> GetDocumentsNonProvidedInAccountingAsync(int firmId, int userId, DateTime startDate, DateTime endDate);       
    }
}