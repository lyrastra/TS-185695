using System;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.EventBus;
using Moedelo.CommonV2.EventBus.Cash;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;

namespace Moedelo.Finances.Api.Controllers
{
    [RoutePrefix("Money/Integration/Test")]
    public class IntegrationTestController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly IPublisher<YandexMovementsRequestedEvent> yandexMovementRequestedPublisher;

        public IntegrationTestController(
            IUserContext userContext,
            IPublisherFactory publisherFactory)
        {
            this.userContext = userContext;
            yandexMovementRequestedPublisher = publisherFactory.GetForAllClient(EventBusMessages.YandexMovementsRequested);
        }

        [HttpPost]
        [Route("RequestYandexMovements")]
        public async Task<IHttpActionResult> RequestYandexMovementsAsync(DateTime? startDate, DateTime? endDate)
        {
            var yandexMovementsRequestedEvent = new YandexMovementsRequestedEvent
            {
                FirmId = userContext.FirmId,
                StartDate = startDate ?? DateTime.Today,
                EndDate = endDate ?? DateTime.Today.AddDays(-1),
            };
            await yandexMovementRequestedPublisher.PublishAsync(yandexMovementsRequestedEvent).ConfigureAwait(false);

            return Ok("Ваш запрос поставлен в очередь на обработку");

        }
    }
}