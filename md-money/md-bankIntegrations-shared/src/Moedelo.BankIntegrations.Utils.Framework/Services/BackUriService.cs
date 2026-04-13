using System.Threading.Tasks;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrations.Models.IntegrationMenu;
using Moedelo.BankIntegrations.Utils.Framework.Abstractions.Services;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.BankIntegrations.Utils.Framework.Services
{
    [InjectAsSingleton(typeof(IBackUriService))]
    public class BackUriService : IBackUriService
    {
        private readonly IIntegrationMenuService integrationMenuService;
        
        public BackUriService(
            IIntegrationMenuService integrationMenuService)
        {
            this.integrationMenuService = integrationMenuService;
        }

        public async Task<BackUrl> GetAsync(int firmId, IntegrationPartners partner)
        {
            var result = new BackUrl();
            result.IntegrationSource = await integrationMenuService.GetAsync(firmId, partner);
            switch (result.IntegrationSource)
            {
                case IntegrationSource.Finances:
                    result.RedirectUrl = "/Finances";
                    break;
                case IntegrationSource.Undefined:
                case IntegrationSource.Requisites:
                default:
                    result.RedirectUrl = "/Requisites/?settlements";
                    break;
            }

            return result;
        }
    }
}