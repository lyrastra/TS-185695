using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.StatisticsSender
{
    public interface IStatisticsSenderClient : IDI
    {
        Task SendForSberbankAsync();

        Task CheckSberbankUpgAsync();

        Task SendEmailAboutExpiriesCertificatesAsync();

        Task SendEmailAboutMorningInfoAsync();
    }
}