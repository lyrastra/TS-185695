using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks;
using Moedelo.Money.Business.Abstractions.SkypeNotifications;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices
{
    /// <summary>
    /// Перерасчет статуса счета (обработка изменения связей счет-платеж)
    /// Эти события должен обрабатывать счет, но на данный момент нет соответствующего приложения 
    /// </summary>
    internal sealed class BillAndPaymentChangeLinkEventsReadHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IBillAndPaymentChangeLinkEventsReader eventsReader;
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISkypeNotificationSender skypeNotificationSender;
        private readonly SettingValue accountingApiEndpoint;
        private readonly SettingValue consumerCountSetting;

        public BillAndPaymentChangeLinkEventsReadHostedService(
            ILogger<BillAndPaymentChangeLinkEventsReadHostedService> logger,
            IBillAndPaymentChangeLinkEventsReader eventsReader,
            IHttpRequestExecuter httpRequestExecuter,
            IExecutionInfoContextAccessor contextAccessor,
            ISettingRepository settingRepository,
            ISkypeNotificationSender skypeNotificationSender)
        {
            this.logger = logger;
            this.eventsReader = eventsReader;
            this.httpRequestExecuter = httpRequestExecuter;
            this.contextAccessor = contextAccessor;
            this.skypeNotificationSender = skypeNotificationSender;
            accountingApiEndpoint = settingRepository.Get("AccountingApiEndpoint");
            consumerCountSetting = settingRepository.Get("BillAndPaymentChangeLinkEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventsReader.ReadAsync(MoneyConstants.GroupId, OnLinkChangedAsync, OnException,
                consumerCount: consumerCount, cancellationToken: cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnLinkChangedAsync(BillAndPaymentChangeLinkMessage message)
        {
            var billBaseIds = new List<long>();

            if (message.CreatedLinks?.Any() == true)
            {
                billBaseIds.AddRange(message.CreatedLinks.Select(l => l.BillBaseId));
            }

            if (message.DeletedLinks?.Any() == true)
            {
                billBaseIds.AddRange(message.DeletedLinks.Select(l => l.BillBaseId));
            }

            return UpdateBillPaymentStatusByBaseIdsAsync(billBaseIds);
        }

        private Task UpdateBillPaymentStatusByBaseIdsAsync(IReadOnlyCollection<long> billBaseIds)
        {
            if (billBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            var endpoint = accountingApiEndpoint.Value;
            var context = contextAccessor.ExecutionInfoContext;
            var uri = $"{endpoint}/Bill/UpdatePaymentStatus?firmId={context.FirmId}&userId={context.UserId}";
            var content = new StringContent(billBaseIds.ToJsonString(), Encoding.UTF8, "application/json");
            return httpRequestExecuter.PostAsync(uri, content);
        }

        private Task OnException(BillAndPaymentChangeLinkMessage message, Exception ex)
        {
            var logger = $"{nameof(BillAndPaymentChangeLinkEventsReadHostedService)}.{nameof(message)}";
            skypeNotificationSender.SendException(logger, ex, message);
            return Task.CompletedTask;
        }
    }
}
