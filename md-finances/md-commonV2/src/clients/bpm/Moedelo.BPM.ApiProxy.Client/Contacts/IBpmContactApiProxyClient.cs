using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BPM.ApiProxy.Client.Contacts
{
    public interface IBpmContactApiProxyClient : IDI
    {
        Task<List<string>> CheckEmailsAllowedAsync(IEnumerable<string> emails);
    }
}