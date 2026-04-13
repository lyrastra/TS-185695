using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.BankIntegrations.Utils.Framework.Abstractions.Redis;
using Moedelo.BankIntegrations.Utils.Framework.Abstractions.Services;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.BankIntegrations.Utils.Framework.Services
{
    [InjectAsSingleton(typeof(IIntegrationMenuService))]
    public class IntegrationMenuService : IIntegrationMenuService
    {
        /// <summary>
        /// Шаблон ключа в редисе
        /// 0 - идентификатор фирмы
        /// 1 - IntegrationPartners
        /// </summary>
        private const string KeyTemplate = "BackUrl:{0}:{1}";

        private readonly IBankIntegrationsRedisDbExecutor redisDbExecutor;

        public IntegrationMenuService(IBankIntegrationsRedisDbExecutor redisDbExecutor)
        {
            this.redisDbExecutor = redisDbExecutor;
        }

        public async Task SetAsync(int firmId, IntegrationPartners partner, IntegrationSource integrationSource)
        {
            var key = GetKey(firmId, partner);
            await redisDbExecutor.SetValueForKeyAsync(key, integrationSource.ToString(), TimeSpan.FromDays(1));
        }
        
        public async Task<IntegrationSource> GetAsync(int firmId, IntegrationPartners partner)
        {
            var key = GetKey(firmId, partner);
            var value = await redisDbExecutor.GetValueByKeyAsync(key);
            if (value != null)
            {
                await redisDbExecutor.DeleteKeyAsync(key);
            }
            Enum.TryParse(value, out IntegrationSource result);

            return result;
        }
        
        private string GetKey(int firmId, IntegrationPartners partner)
        {
            return string.Format(KeyTemplate, firmId, partner);
        }
    }
}