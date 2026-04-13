using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BPM.Emails.Client
{
    public interface IBpmEmailsClient : IDI
    {
        Task<bool> MailParseAsync(string email, string backup_email = null, int count = 1);
        Task ClientMailParseAsync();
    }
}