using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.BankBalanceHistory.Api.Mappers;
using Moedelo.Money.BankBalanceHistory.Api.Models;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances;

namespace Moedelo.Money.BankBalanceHistory.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/Balances")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BalancesPrivateController : ControllerBase
    {
        private readonly IBalancesReader balancesReader;

        public BalancesPrivateController(IBalancesReader balancesReader)
        {
            this.balancesReader = balancesReader;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAsync([FromQuery] BankBalanceRequestDto requestDto)
        {
            var request = BalancesMapper.Map(requestDto);
            var response = await balancesReader.GetAsync(request);
            var responseDto = BalancesMapper.Map(response);
            return new ApiDataResult(responseDto);
        }

        [AllowAnonymous]
        [HttpPost("OnDateByFirms")]
        public async Task<IActionResult> OnDateByFirmsAsync(BankBalancesOnDateByFirmsRequestDto requestDto)
        {
            var response = await balancesReader
                .GetOnDateByFirmIdsAsync(requestDto.FirmIds,
                    requestDto.OnDate,
                    requestDto.MinDate,
                    requestDto.IncludeUserMovement);
            var responseDto = BalancesMapper.Map(response);
            return new ApiDataResult(responseDto);
        }
    }
}
