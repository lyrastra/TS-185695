using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Public.Mappers;

namespace Moedelo.Finances.Public.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ContractorsController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IMoneyContractorsService contractorsService;

        public ContractorsController(
            IUserContext userContext,
            IMoneyContractorsService contractorsService)
        {
            this.userContext = userContext;
            this.contractorsService = contractorsService;
        }

        [HttpGet]
        [Route("Money/Contractors")]
        public async Task<IHttpActionResult> GetAsync(string query = "", int count = 5,
            MoneyContractorType type = MoneyContractorType.All,
            CancellationToken cancellationToken = default)
        {
            var contractors = await contractorsService
                .GetAsync(userContext.FirmId, userContext.UserId, query, count, type, cancellationToken)
                .ConfigureAwait(false);
            return Data(ContractorMapper.Map(contractors));
        }

        [HttpGet]
        [Route("Contractors/{id:int}")]
        public async Task<IHttpActionResult> GetAsync(int id, MoneyContractorType type = MoneyContractorType.All)
        {
            var contractor = await contractorsService
                .GetByIdAsync(userContext.FirmId, userContext.UserId, id, type)
                .ConfigureAwait(false);
            return Data(contractor != null ? ContractorMapper.Map(contractor) : null);
        }
    }
}