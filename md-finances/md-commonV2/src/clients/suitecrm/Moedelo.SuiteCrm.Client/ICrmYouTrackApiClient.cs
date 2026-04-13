using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.SuiteCrm.Client
{
    public interface ICrmYouTrackApiClient : IDI
    {
        Task CreateIssueOnCaseAsync(string caseId, string creatorName);

        Task ChangeIssueStatusAsync(string issueId, string issueStatus);
    }
}
