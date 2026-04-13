using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.CommonV2.Auth.Wsse.Business.Mappers;
using Moedelo.CommonV2.Auth.Wsse.Domain.Interfaces;
using Moedelo.CommonV2.Auth.Wsse.Domain.Models;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.Auth.Wsse.Business.Services
{
    [Inject(InjectionType.Singleton)]
    public class ExternalPartnerService : IExternalPartnerService
    {
        private readonly IExternalPartnerDao externalPartnerDao;

        public ExternalPartnerService(IExternalPartnerDao externalPartnerDao)
        {
            this.externalPartnerDao = externalPartnerDao;
        }

        public async Task<List<ExternalPartnerCredential>> GetCredentialAsync()
        {
            var result = await externalPartnerDao.GetAsync().ConfigureAwait(false);
            var credentials = result?.Select(ExternalPartnerMapper.Map).ToList();

            return credentials;
        }
    }
}
