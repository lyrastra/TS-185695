using Microsoft.AspNetCore.Mvc;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Authorization;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.Api.Mappers;
using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.Registry;
using Moedelo.Money.Business.Abstractions.Registry;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Domain.Registry;

namespace Moedelo.Money.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [HasAllAccessRules(AccessRule.UsnAccountantTariff)]
    public class RegistryController : ControllerBase
    {
        private readonly IRegistryReader registryService;

        public RegistryController(IRegistryReader registryService)
        {
            this.registryService = registryService;
        }

        /// <summary>
        /// Получение списка операций
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiPageResponseDto))]
        [SwaggerOperation(Tags = new[] { "Деньги - Реестр операций" })]
        public Task<IActionResult> GetAsync([FromQuery] RegistryQueryDto request)
        {
            var query = RegistryMapper.MapToDomain(request);
            return SelectAsync(query);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiPageResponseDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public Task<IActionResult> PostAsync([FromBody] PrivateRegistryQueryDto request)
        {
            var query = RegistryMapper.MapToDomain(request);
            return SelectAsync(query);
        }

        private async Task<IActionResult> SelectAsync(RegistryQuery query)
        {
            var response = await registryService.GetAsync(query).ConfigureAwait(false);
            var operations = response.Operations.Select(RegistryMapper.MapOperationToDto).ToArray();

            return new ApiPageResult(
                operations,
                response.Offset,
                response.Limit,
                response.TotalCount);
        }
    }
}
