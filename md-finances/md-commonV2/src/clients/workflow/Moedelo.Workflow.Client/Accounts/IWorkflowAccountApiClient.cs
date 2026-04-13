using Moedelo.Workflow.Dto.Accounts;
using System.Threading.Tasks;

namespace Moedelo.Workflow.Client.Accounts;

public interface IWorkflowAccountApiClient
{
    Task<AccountDto> CreateAsync(AccountDto model);
}