using System.Threading.Tasks;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrations.Models.IntegrationMenu;

namespace Moedelo.BankIntegrations.Utils.Framework.Abstractions.Services
{
    public interface IBackUriService
    {
        Task<BackUrl> GetAsync(int firmId, IntegrationPartners partner);
    }
}