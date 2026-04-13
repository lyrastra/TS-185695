using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Integrations
{
    public interface IStatementRequestService : IDI
    {
        Task<BankStatementResponse> SendStatementRequestsAsync(IUserContext userContext, BankStatementRequestBySettlementAccounts request);
        
        Task<List<ResultOfStatementRequest>> SendStatementRequestsVerboseAsync(IUserContext userContext, BankStatementRequestBySettlementAccounts request);

        Task<BankStatementResponse> SendStatementRequestAsync(IUserContext userContext, BankStatementRequestBySettlementAccount request);

        Task<BankStatementResponse> SendStatementRequestAsync(IUserContext userContext, BankStatementRequestByIntegrationPartner request);

        Task<BankStatementResponse> SendPurseStatementRequestAsync(IUserContext userContext, StatementRequestByPurse request);
    }
}
