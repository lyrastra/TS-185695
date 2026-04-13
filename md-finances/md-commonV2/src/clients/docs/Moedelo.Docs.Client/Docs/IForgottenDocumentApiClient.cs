using Moedelo.Docs.Dto.Docs;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.Docs
{
    public interface IForgottenDocumentApiClient : IDI
    {
        Task<List<long>> GetByDate(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<List<long>> GetByForgottenDate(int firmId, int userId, DateTime startDate, DateTime endDate);

        Task<List<ForgottenDocumentDto>> GetByBaseIds(int firmId, int userId, List<long> baseIds);
    }
}
