using System;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.Auth.WebApi;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.ClosedCashGroup;

namespace Moedelo.Finances.Api.Controllers
{
    [RoutePrefix("ClosedCashGroup")]
    [WebApiRejectUnauthorizedRequest]
    public class ClosedCashGroupController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly ICloseCashGroupService closeCashGroupService;

        public ClosedCashGroupController(ICloseCashGroupService closeCashGroupService, IUserContext userContext)
        {
            this.closeCashGroupService = closeCashGroupService;
            this.userContext = userContext;
        }

        /// <summary>
        /// Возвращает последнюю дату закрытой кассы для пользователей БИЗ
        /// </summary>
        /// <returns>Возвращает дату или, если касса еще не закрывалась - null</returns>
        [HttpGet]
        [Route("LastClosedCashDate")]
        public async Task<DateTime?> GetLastClosedCashDateAsync()
        {
            var result = await closeCashGroupService.GetLastClosedCashDateAsync(userContext.FirmId).ConfigureAwait(false);
            return result;
        }

    }
}
