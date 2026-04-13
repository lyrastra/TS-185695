using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.CommonV2.Auth.Wsse.DataAccess.Queries;
using Moedelo.CommonV2.Auth.Wsse.Domain.Interfaces;
using Moedelo.CommonV2.Auth.Wsse.Domain.Models;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.CommonV2.Auth.Wsse.DataAccess
{
    [Inject(InjectionType.Singleton)]
    public class ExternalPartnerDao : IExternalPartnerDao
    {
        private readonly IMoedeloReadOnlyDbExecutor moedeloReadOnlyDbExecutor;

        public ExternalPartnerDao(IMoedeloReadOnlyDbExecutor moedeloReadOnlyDbExecutor)
        {
            this.moedeloReadOnlyDbExecutor = moedeloReadOnlyDbExecutor;
        }

        public async Task<List<ExternalPartnerCredentialDbResult>> GetAsync()
        {
            var queryObject = new QueryObject(ExternalPartnerQueries.GetCredential);
            var result = await moedeloReadOnlyDbExecutor.QueryAsync<ExternalPartnerCredentialDbResult>(queryObject).ConfigureAwait(false);

            return result.ToList();
        }
    }
}
