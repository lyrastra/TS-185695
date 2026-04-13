using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Mvc.ActionResults;
using Moedelo.Money.BankBalanceHistory.Api.Mappers;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances;
using System.Linq;
using Moedelo.Money.BankBalanceHistory.Api.Models;

namespace Moedelo.Money.BankBalanceHistory.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BalancesController : ControllerBase
    {
        private readonly IBalancesReader balancesReader;
        private readonly IBalanceUpdater balanceUpdater;

        public BalancesController(IBalancesReader balancesReader, 
            IBalanceUpdater balanceUpdater)
        {
            this.balancesReader = balancesReader;
            this.balanceUpdater = balanceUpdater;
        }

        [HttpGet("OnDateByFirm")]
        public async Task<IActionResult> OnDateByFirmAsync(DateTime onDate, bool includeUserMovement = false)
        {
            var response = await balancesReader.GetOnDateByFirmIdAsync(onDate, includeUserMovement: includeUserMovement);
            var responseDto = response.Select(BalancesMapper.Map);
            return new ApiDataResult(responseDto);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(BalanceRequestDto requestDto)
        {
            var request = BalancesMapper.Map(requestDto);

            await balanceUpdater.UpdateAsync([request]);
            
            return Ok();
        }
    }
}
