using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models;

namespace Moedelo.Finances.Domain.Interfaces.Business.Data;

public interface ISetupDataService
{
    Task<SetupData> GetAsync(IUserContext userContext, CancellationToken cancellationToken);
}